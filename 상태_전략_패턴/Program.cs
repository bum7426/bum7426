using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//전략 패턴

/*
enum Char_Type {Warrior = 1, Archer, Elf};
public abstract class Character
{
    public Attack att;
    public Defence def;

    public void Attack()
    {
        att.Att();
    }

    public void Def()
    {
        def.Defence();
    }

    public abstract void Voice();
}

public class Warrior : Character
{
    public Warrior()
    {
        att = new tar_att();
        def = new P_Defence();
        Console.WriteLine("전사가 생성되었습니다");
    }

    public override void Voice()
    {
        Console.WriteLine("전사는 역시 야만용사!!");
    }
}

public class Ahcher : Character
{
    public Ahcher()
    {
        att = new nontar_att();
        def = new P_Defence();
        Console.WriteLine("궁수가 생성되었습니다");
    }

    public override void Voice()
    {
        Console.WriteLine("주몽의 후예!");
    }
}

public class Elf : Character
{
    public Elf()
    {
        att = new nontar_att();
        def = new M_Defence();
        Console.WriteLine("엘프가 생성되었습니다");
    }

    public override void Voice()
    {
        Console.WriteLine("엘프 존예임");
    }
}


public interface Attack
{
    void Att();
}

public class tar_att : Attack
{
    public void Att()
    {
        Console.WriteLine("적 직접 타격");
    }
}

public class nontar_att : Attack
{
    public void Att()
    {
        Console.WriteLine("논타겟 타격");
    }
}


public interface Defence
{
    void Defence();
}

public class P_Defence : Defence
{
    public void Defence()
    {
        Console.WriteLine("물리방어");
    }
}

public class M_Defence : Defence
{
    public void Defence()
    {
        Console.WriteLine("마법방어");
    }
}


class Program
{
    static void Main(string[] args)
    {
        Character Player = null;
        bool IsPlaying = true;

        int Input;

        while (IsPlaying)
        {
            Console.WriteLine("1. 직업 선택");
            Console.WriteLine("2. 공격");
            Console.WriteLine("3. 방어");
            Console.WriteLine("4. 직업 대사");
            Console.WriteLine("5. 종료");

            Input = int.Parse(Console.ReadLine());

            Console.Clear();

            switch (Input)
            {
                case 1:
                    Console.WriteLine("1. 전사");
                    Console.WriteLine("2. 궁수");
                    Console.WriteLine("3. 엘프");

                    Input = int.Parse(Console.ReadLine());

                    Console.Clear();

                    switch ((Char_Type)Input)
                    {
                        case Char_Type.Warrior:
                            Player = new Warrior();
                            break;

                        case Char_Type.Archer:
                            Player = new Ahcher();
                            break;

                        case Char_Type.Elf:
                            Player = new Elf();
                            break;
                    }
                    break;

                case 2:
                    if (Player == null) Console.WriteLine("직업이 선택되지 않았습니다.");
          
                    else Player.Attack();
                    break;
                    
                case 3:
                    if (Player == null) Console.WriteLine("직업이 선택되지 않았습니다.");
                    
                    else Player.Def();

                    break;

                case 4:
                    if (Player == null) Console.WriteLine("직업이 선택되지 않았습니다.");

                    else Player.Voice();                    

                    break;

                case 5:
                    IsPlaying = false;
                    break;
            }
        }        
    }
}
*/



/// /////////////////////////////////////////////////////////////
// 상태 패턴

class Player
{
    P_State Status;
    public Player()
    {
        Status = new InitState();
    }

    public void Play()
    {
        bool IsPlaying = true;
        int Input;

        while (IsPlaying)
        {
            Console.WriteLine("1. 플레이어 생성!!");
            Console.WriteLine("2. 너는 강해졌다!!");
            Console.WriteLine("3. 약해졌어......시무룩");
            Console.WriteLine("4. 공격 쳐맞음");
            Console.WriteLine("5. 종료");

            Input = int.Parse(Console.ReadLine());

            Console.Clear();

            switch (Input)
            {
                case 1: Status = new InitState();
                    Status.State();
                    break;
                case 2: Status = new BuffState();
                    Status.State();
                    break;
                case 3: Status = new DeBuffState();
                    Status.State();
                    break;
                case 4: Status = new AttackedState();
                    Status.State();
                    break;
                case 5:
                    IsPlaying = false;
                    break;
            }
        }
    }  
}

public abstract class P_State
{
    protected static int _HP = 100;
    protected static int _Ability = 100;
    public abstract void State();
}

public class InitState : P_State
{
    public override void State()
    {
        _HP = 100;
        _Ability = 100;
        Console.WriteLine("Player HP : " + _HP);
        Console.WriteLine("Player Ability : " + _Ability);
    }   
}

public class BuffState : P_State
{
    public override void State()
    {
        _HP += 10;
        _Ability += 20;
        Console.WriteLine("Player HP : " + _HP);
        Console.WriteLine("Player Ability : " + _Ability);
    }
}

public class DeBuffState : P_State
{
    public override void State()
    {
        _HP -= 10;
        _Ability -= 20;
        if (_HP <= 0)
        {
            _HP = 0;
            _Ability = 0;
            Console.WriteLine("Player HP : " + _HP);
            Console.WriteLine("Player Ability : " + _Ability);
            Console.WriteLine("으앙 쥬금");
            return;
        }
        else if (_Ability <= 0)
        {
            _Ability = 0;
            Console.WriteLine("Player HP : " + _HP);
            Console.WriteLine("Player Ability : " + _Ability);
            Console.WriteLine("더 이상 깍일 능력치가 없다");
            return;
        }
        else
        {
            Console.WriteLine("Player HP : " + _HP);
            Console.WriteLine("Player Ability : " + _Ability);
        }
    }
}

public class AttackedState : P_State
{
    public override void State()
    {
            _HP -= 20;
            if (_HP <= 0)
            {
                _HP = 0;
                _Ability = 0;
                Console.WriteLine("Player HP : " + _HP);
                Console.WriteLine("Player Ability : " + _Ability);
                Console.WriteLine("으앙 쥬금");
                return;
            }
            else if (_Ability <= 0)
            {
                _Ability = 0;
                Console.WriteLine("Player HP : " + _HP);
                Console.WriteLine("Player Ability : " + _Ability);
                Console.WriteLine("더 이상 깍일 능력치가 없다");
                return;
            }
            else
            {
                Console.WriteLine("Player HP : " + _HP);
                Console.WriteLine("Player Ability : " + _Ability);
            }
        
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("이거슨 상태패턴입니다");
        Player test = new Player();

        test.Play();

    }
}