using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 基本代码
{
    class Program
    {
        static void Main(string[] args)
        {
            Context context;

            //调用具体算法a
            context = new Context(new ConcreateStrategyA());
            context.ContextInterface();

            //调用具体算法b
            context = new Context(new ConcreateStrategyB());
            context.ContextInterface();

            //调用具体算法c
            context = new Context(new ConcreateStrategyC());
            context.ContextInterface();

            Console.Read();
        }
    }

    //抽象算法类
    abstract class Strategy
    {
        //算法方法
        public abstract void AlgorithmInterface();
    }


    //具体的算法A
    class ConcreateStrategyA:Strategy
    {
        public override void AlgorithmInterface()
        {
            Console.WriteLine("具体的算法A");
        }
    }


    //具体的算法B
    class ConcreateStrategyB : Strategy
    {
        public override void AlgorithmInterface()
        {
            Console.WriteLine("具体的算法B");
        }
    }


    //具体的算法C
    class ConcreateStrategyC : Strategy
    {
        public override void AlgorithmInterface()
        {
            Console.WriteLine("具体的算法C");
        }
    }


    //上下文类
    class Context
    {
        readonly Strategy _strategy;

        public Context(Strategy strategy)
        {
            _strategy = strategy;
        }

        public void ContextInterface()
        {
            _strategy.AlgorithmInterface();//调用具体的算法的接口；
        }
    }


}
