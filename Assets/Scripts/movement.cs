using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;






public class movement : MonoBehaviour
{

    public int reward = 0;
    public class FinishedCup
    {
        public bool is_ready() { return (coffee != null); }

        public void set_coffee(CoffeeType other) { coffee = other; }

        public CoffeeType get_coffee() { return coffee; }
        
        

        private CoffeeType? coffee = null;

        public CupOfCoffee pre_cup = new CupOfCoffee();
    }

    public FinishedCup order_cup = new FinishedCup();

    public void add_espresso()
    {
        order_cup.pre_cup.ingredients.Add(Ingredients.Espresso);
    } 

    public void add_milk()
    {
        order_cup.pre_cup.ingredients.Add(Ingredients.Milk);
    }

#if DEBUG
    SpriteRenderer square;
#endif

    private Animator maxatik;

    private SpriteRenderer blinding_screen_renderer;

    // ������� ����� ��� ���������� ��� �������.
    public static KeyCode _left_leg = KeyCode.F;
    public static KeyCode _right_leg = KeyCode.G;
    public static KeyCode _left_arm = KeyCode.J;
    public static KeyCode _right_arm = KeyCode.K;
    public static KeyCode _breath = KeyCode.O;
    public static KeyCode _blink = KeyCode.E;
    public static KeyCode _regain_posture = KeyCode.Space;
    public static KeyCode _change_direction = KeyCode.H;


    public KeyCode _action_button = KeyCode.X;


    public Vector3 direction = Vector3.right;

    // ����� �������, ������� ��������� ����� �� ������.
    public double breath_genkai = 10;

    // ����� �������, ������� ��������� ����� �� �������.
    public double blink_genkai = 10;

    public double blinking_offset_when_screen_starts_fading = 2;
    public double blink_fading_speed = 5;

    private bool is_colliding = false;

    private Actionable current_action;

    
    // !!!�� �������!!! �������� �����.
    private class Arm<T>
    {
        public Arm(KeyCode assign_key) { assigned_key = assign_key; }   
        public bool is_empty() { return !holds_anything; }
        public void take(T obj) { held_object = obj; holds_anything = true; }

        public KeyCode get_assigned_key() { return assigned_key; }

        private T held_object = default(T);
        private bool holds_anything = false;
        private KeyCode assigned_key;

    }

    private Arm<Pickable> left_arm = new(_left_arm);
    private Arm<Pickable> right_arm = new(_right_arm);


    public void pickUp(Pickable obj, KeyCode which_arm)
    {
        if (which_arm == left_arm.get_assigned_key())
        {
            if (left_arm.is_empty()) { left_arm.take(obj); obj.onPick(this); }
            
        }
        else if (which_arm == right_arm.get_assigned_key()) 
        {
            if (right_arm.is_empty()) { right_arm.take(obj); obj.onPick(this); }
           
        }
       
        
    }
    // �������� �� ��, ����� ����������: ����� ��� ������.
    // ��� ���� ����� ���������� �������� �����, ������� ��������� ��� �������������� �������.
    public enum Limb : System.UInt16 { Right = 0x0, Left = 0xFFFF}

    // ������ � ���� ��� ������� ��������, ����� ������� � ��� �����.
    // !!!������ ����� ����� �������������!!!
    public class Actions
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

    public Actions action = new();


    Blinking blink_obj = null;

    /// <summary>
    /// ������� ���������, ���� ������ ���� �� ����� ��, ��� ����������.
    /// !!!� debug ������!!! ����� ������ ���� ������.
    /// </summary>
    /// <param name="dir">���� ��������� ��������</param>
    /// <param name="prev_limb">���������� ����</param>
    /// <param name="curr_limb">����, �� ������� �������� �������� ������</param>
    private void moveLeg(Vector3 dir, Limb prev_limb, Limb curr_limb)
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
            transform.position += dir;
            action.leg = Limb.Left;
        }
        else
        {
#if DEBUG
            square.color = Color.blue;
#endif
            transform.position += dir;
            action.leg = Limb.Right;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
#if DEBUG
        square = GetComponent<SpriteRenderer>();
        order_cup.set_coffee(CoffeeType.Cappuccino);
#endif
        //maxatik = transform.GetChild(0).gameObject.GetComponent<Animator>();
        blink_obj = FindObjectOfType<Blinking>();
        Color tmp = blink_obj.GetComponent<SpriteRenderer>().color;
        tmp.a = 0;
        blink_obj.GetComponent<SpriteRenderer>().color = tmp;
        //maxatik.Play("Idle");
        
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
            moveLeg(direction, action.leg, Limb.Right);
           // maxatik.Play("right_after_left");
        }
        else if (Input.GetKeyDown(_left_leg))
        {
            moveLeg(direction, action.leg, Limb.Left);
            //maxatik.Play("left_after_raight");
        }

        // Try
        if (Input.GetKeyDown(_change_direction)) direction = -direction;

        if (Input.GetKeyDown(_breath)) action.breath();

        if (Input.GetKeyDown(_blink)) 
        {
            
            if (blink_obj)
            {
                blink_obj.res();
            }
            action.blink(); 
        }

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


        if (is_colliding) 
        {
           
            current_action.actOn(this, _action_button);
            
        }

        // ������ ������� � ������� ��� ������ � ��� ������, ���� ��� ��������� ��� �����.
        if (action.isBreathing())
        {
            action.time_since_breath += Time.deltaTime;
        }
        action.time_since_blink += Time.deltaTime;
    }



  
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (LayerMask.LayerToName(other.gameObject.layer) == "Actions" || LayerMask.LayerToName(other.gameObject.layer) == "OrderTrigger")
        {
            
            is_colliding = true;
            Actionable act = other.gameObject.GetComponent<Actionable>();
            act.preActOn(this);
            current_action = act;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        current_action.postActOn(this);
        is_colliding = false;
        current_action = null;
    }
}
