using UnityEngine;






public class movement : MonoBehaviour
{
#if DEBUG
    SpriteRenderer square;
#endif

    // Закидка кодов для управления для эдитора.
    public KeyCode _left_leg = KeyCode.F;
    public KeyCode _right_leg = KeyCode.G;
    public KeyCode _left_arm = KeyCode.J;
    public KeyCode _right_arm = KeyCode.K;
    public KeyCode _breath = KeyCode.O;
    public KeyCode _blink = KeyCode.E;
    public KeyCode _regain_posture = KeyCode.Space;


    // Лимит времени, сколько человечек может не дышать.
    public double breath_genkai = 10;

    // Лимит времени, сколько человечек может не моргать.
    public double blink_genkai = 10;



    // !!!НЕ ТРОГАТЬ!!! тестовый класс.
    private class Arm
    {
        public Object held_object;
    }

    // Отвечает за то, какая конечность: левая или правая.
    // При этом левая конечность является кодом, который получится при инвертировании правого.
    enum Limb : System.UInt16 { Right = 0x0, Left = 0xFFFF}

    // Хранит в себе все базовые действия, вроде дыхания и так далее.
    // !!!Скорее всего стоит переименовать!!!
    class Actions
    {
        // Какая нога и рука были использованы последними
        public Limb leg = Limb.Right;
        public Limb arm = Limb.Right;

        // Время, прошедшее с последнего моргания или дыхания
        // !!!Может стоит переписать!!! как... Сделать enum для действий. А тут хранить только вроде... time_since и действие. Но даже не знаю.
        public double time_since_blink = 0;
        public double time_since_breath = 0;

        // Дышит ли персонаж
        private bool breathing = true;
        // В равновесии ли персонаж.
        public bool standing = true;

        /// <summary>
        /// Обнуляет время моргания.
        /// </summary>
        /// <returns>Время, которое прошло с предыдущего моргания</returns>
        public double blink()
        {
            double temp = time_since_blink;
            time_since_blink = 0;
            return temp;
        }

        /// <summary>
        /// Обнуляет время дыхания.
        /// </summary>
        /// <returns>Время, которое прошло с предыдущего вдоха</returns>
        public double breath()
        {
            if (!isBreathing()) return 0;
            double temp = time_since_breath;
            time_since_breath = 0;
            return temp;
        }

        /// <summary>
        /// Прекращает дыхание персонажа.
        /// </summary>
        /// <param name="should_cache">Если true, то время с предыдущего вдоха сохранится</param>
        public void stopBreathing(bool should_cache = false)
        {
            if (!should_cache)
            {
                time_since_breath = 0;
            }
            breathing = false;
        }

        /// <summary>
        /// Разрешает игроку дышать.
        /// </summary>
        public void startBreathing()
        {
            breathing = true;
        }

        /// <summary>
        /// Вставай, на работу пора.
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
    /// Двигает персонажа, если данная нога не такая же, как предыдущая.
    /// !!!В debug режиме!!! также меняет цвет кубика.
    /// </summary>
    /// <param name="direction">Куда сдвинется персонаж</param>
    /// <param name="prev_limb">Предыдущая нога</param>
    /// <param name="curr_limb">Нога, на которую наступит персонаж сейчас</param>
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

        // Скейлим альфа канал спрайта в зависимости от того, как долго он не дышал.
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
            // Если игрок находился в упавшем состоянии, то мы встаём и начинаем дышать снова.
            // Иначе же просто ничего не делаем.
            if (!action.standing)
            {
                action.regainPoisture();
                action.startBreathing();
            }
        }
        // По истечении того, сколько наш человечек может не дышать, мы скидываем его, а также прекращаем отсчёт времени
        if (action.time_since_breath > (breath_genkai - Mathf.Epsilon))
        {
            action.standing = false;
            action.stopBreathing();

        }

        // Отсчёт времени с дыхания идёт только в том случае, если наш человечек уже дышал.
        if (action.isBreathing())
        {
            action.time_since_breath += Time.deltaTime;
        }
        action.time_since_blink += Time.deltaTime;
    }
}
