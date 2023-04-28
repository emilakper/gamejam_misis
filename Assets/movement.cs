using UnityEngine;






public class movement : MonoBehaviour
{
#if DEBUG
    SpriteRenderer square;
#endif

    // ������� ����� ��� ���������� ��� �������.
    public KeyCode _left_leg = KeyCode.F;
    public KeyCode _right_leg = KeyCode.G;
    public KeyCode _left_arm = KeyCode.J;
    public KeyCode _right_arm = KeyCode.K;
    public KeyCode _breath = KeyCode.O;
    public KeyCode _blink = KeyCode.E;
    public KeyCode _regain_posture = KeyCode.Space;


    // ����� �������, ������� ��������� ����� �� ������.
    public double breath_genkai = 10;

    // ����� �������, ������� ��������� ����� �� �������.
    public double blink_genkai = 10;



    // !!!�� �������!!! �������� �����.
    private class Arm
    {
        public Object held_object;
    }

    // �������� �� ��, ����� ����������: ����� ��� ������.
    // ��� ���� ����� ���������� �������� �����, ������� ��������� ��� �������������� �������.
    enum Limb : System.UInt16 { Right = 0x0, Left = 0xFFFF}

    // ������ � ���� ��� ������� ��������, ����� ������� � ��� �����.
    // !!!������ ����� ����� �������������!!!
    class Actions
    {
        // ����� ���� � ���� ���� ������������ ����������
        public Limb leg = Limb.Right;
        public Limb arm = Limb.Right;

        // �����, ��������� � ���������� �������� ��� �������
        // !!!����� ����� ����������!!! ���... ������� enum ��� ��������. � ��� ������� ������ �����... time_since � ��������. �� ���� �� ����.
        public double time_since_blink = 0;
        public double time_since_breath = 0;

        // ����� �� ��������
        private bool breathing = true;
        // � ���������� �� ��������.
        public bool standing = true;

        /// <summary>
        /// �������� ����� ��������.
        /// </summary>
        /// <returns>�����, ������� ������ � ����������� ��������</returns>
        public double blink()
        {
            double temp = time_since_blink;
            time_since_blink = 0;
            return temp;
        }

        /// <summary>
        /// �������� ����� �������.
        /// </summary>
        /// <returns>�����, ������� ������ � ����������� �����</returns>
        public double breath()
        {
            if (!isBreathing()) return 0;
            double temp = time_since_breath;
            time_since_breath = 0;
            return temp;
        }

        /// <summary>
        /// ���������� ������� ���������.
        /// </summary>
        /// <param name="should_cache">���� true, �� ����� � ����������� ����� ����������</param>
        public void stopBreathing(bool should_cache = false)
        {
            if (!should_cache)
            {
                time_since_breath = 0;
            }
            breathing = false;
        }

        /// <summary>
        /// ��������� ������ ������.
        /// </summary>
        public void startBreathing()
        {
            breathing = true;
        }

        /// <summary>
        /// �������, �� ������ ����.
        /// </summary>
        public void regainPoisture()
        {
            standing = true;
        }

        public bool isBreathing()
        {
            return breathing;
        }
    }

    private Actions action = new();


    /// <summary>
    /// ������� ���������, ���� ������ ���� �� ����� ��, ��� ����������.
    /// !!!� debug ������!!! ����� ������ ���� ������.
    /// </summary>
    /// <param name="direction">���� ��������� ��������</param>
    /// <param name="prev_limb">���������� ����</param>
    /// <param name="curr_limb">����, �� ������� �������� �������� ������</param>
    private void moveLeg(Vector3 direction, Limb prev_limb, Limb curr_limb)
    {
        if (prev_limb == curr_limb)
        {
            return;
        }

        if (!action.standing)
        {
            return;
        }

        if (prev_limb == Limb.Right)
        {
#if DEBUG
            square.color = Color.red;
#endif
            transform.position += direction;
            action.leg = Limb.Left;
        }
        else
        {
#if DEBUG
            square.color = Color.blue;
#endif
            transform.position += direction;
            action.leg = Limb.Right;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
#if DEBUG
        square = GetComponent<SpriteRenderer>();
#endif
    }


    // Returns time since last blink


    // Update is called once per frame
    void Update()
    {

#if DEBUG

        // ������� ����� ����� ������� � ����������� �� ����, ��� ����� �� �� �����.
        if (breath_genkai - action.time_since_breath > 0)
        {
            Color b = square.color;
            b.a = (float)(breath_genkai - action.time_since_breath) / 5;
            square.color = b;
        } else square.color = Color.white;
#endif

        if (Input.GetKeyDown(_right_leg))
        {
            moveLeg(Vector3.right, action.leg, Limb.Right);
        }
        else if (Input.GetKeyDown(_left_leg))
        {
            moveLeg(Vector3.right, action.leg, Limb.Left);
        }

        if (Input.GetKeyDown(_breath)) action.breath();

        if (Input.GetKeyDown(_blink)) action.blink();


        if (Input.GetKeyDown(_regain_posture))
        {
            // ���� ����� ��������� � ������� ���������, �� �� ����� � �������� ������ �����.
            // ����� �� ������ ������ �� ������.
            if (!action.standing)
            {
                action.regainPoisture();
                action.startBreathing();
            }
        }
        // �� ��������� ����, ������� ��� ��������� ����� �� ������, �� ��������� ���, � ����� ���������� ������ �������
        if (action.time_since_breath > (breath_genkai - Mathf.Epsilon))
        {
            action.standing = false;
            action.stopBreathing();

        }

        // ������ ������� � ������� ��� ������ � ��� ������, ���� ��� ��������� ��� �����.
        if (action.isBreathing())
        {
            action.time_since_breath += Time.deltaTime;
        }
        action.time_since_blink += Time.deltaTime;
    }
}
