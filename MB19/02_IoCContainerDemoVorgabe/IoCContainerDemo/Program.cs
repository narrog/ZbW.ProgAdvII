using System;
using System.Collections.Generic;
using System.Linq;

namespace IoCContainerDemo {
    class Program {
        static void Main(string[] args) {
            // 1. Variante (einfache Dependency Injection ohne Container)
            //     Shopper ist nicht abhängig von MasterCard - nur vom Interface ICreditCard
            //ICreditCard creditCard = new MasterCard();
            //var shopper = new Shopper(creditCard);
            //shopper.Charge();
            // Nachteil: Jeder Code der Shopper verwendet muss wissen, wie 
            //           ICreditCard erstellt werden muss

            // ******************************************************************************************

            // 2. Variante mit Factory - Logik gekapselt in Factory
            //Resolver resolver = new Resolver();
            //ICreditCard creditCard = resolver.ResolveCreditCard();
            //var shopper = new Shopper(creditCard);
            //shopper.Charge();

            // ******************************************************************************************

            // 3. Variante Version mit einem Container
            // Der Container soll uns ein gewünschtes Objekt inkl. aller Abhängigkeiten erstellen
            Resolver resolver = new Resolver();
            resolver.Register<ICreditCard, MasterCard>();
            resolver.Register<ITerminal,EftTerminal>();
            resolver.Register<Shopper, Shopper>();
            var shopper = resolver.Resolve<Shopper>();
            shopper.Charge();


            Console.Read();
        }
    }

    public class Resolver
    {
        //public ICreditCard ResolveCreditCard()
        //{
        //    var terminal = this.ResolveTerminal();
        //    // Logik für Erstellung ICreditCard zentral in Factory
        //    if(new Random().Next(2) == 1)
        //    {
        //        return new Visa(terminal);
        //    }
        //    return new MasterCard(terminal);
        //}
        //public ITerminal ResolveTerminal()
        //{
        //    return new EftTerminal();
        //}

        private readonly Dictionary<Type, Type> dependencyMap = new Dictionary<Type, Type>();
        public void Register<TFrom, Tto>()
        {
            this.dependencyMap.Add(typeof(TFrom), typeof(Tto));
        }
        public T Resolve<T>()
        {
            var type = typeof(T);
            return (T)this.Resolve(type);
        }

        private object Resolve(Type typeToResolve)
        {
            try
            {
                typeToResolve = this.dependencyMap[typeToResolve];
            } 
            catch 
            {
                throw new Exception($"Could not resolve type {typeToResolve.FullName}. Check Registrations.");
            }
            var firstCtor = typeToResolve.GetConstructors().First();
            var parameters = firstCtor.GetParameters();
            if (parameters.Length == 0)
            {
                return Activator.CreateInstance(typeToResolve);
            }
            var paraList = new List<object>();
            foreach (var parameter in parameters)
            {
                var paraType = parameter.ParameterType;
                var paraValue = this.Resolve(paraType);
                paraList.Add(paraValue);
            }
            return firstCtor.Invoke(paraList.ToArray());
        }
    }

    public abstract class CreditCard : ICreditCard {
        private readonly ITerminal terminal;
        public CreditCard(ITerminal terminal)
        {
            this.terminal = terminal;
        }

        public abstract string Charge();
        public void TrxTerminal()
        {
            this.terminal.Trx();
        }
    }
    public class Visa : CreditCard {
        public Visa(ITerminal terminal) : base(terminal)  { }
        public override string Charge() {
            this.TrxTerminal();
            return "Chaaaarging with the Visa!";
        }
    }

    public class MasterCard : CreditCard {
        public MasterCard(ITerminal terminal) : base(terminal) { }
        public override string Charge() {
            this.TrxTerminal();
            return "Swiping the MasterCard!";
        }
    }


    public class Shopper {
        private readonly ICreditCard creditCard;

        public Shopper(ICreditCard creditCard) {
            this.creditCard = creditCard;
        }

        public void Charge() {
            var chargeMessage = creditCard.Charge();
            Console.WriteLine(chargeMessage);
        }
    }

    public class EftTerminal : ITerminal
    {
        public void Trx()
        {
            Console.WriteLine("Do Eft Transaction");
        }
    }

    public interface ICreditCard {
        string Charge();
    }

    public interface ITerminal
    {
        void Trx();
    }
}
