using System;
using System.Collections.Generic;
using System.Security.Policy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FizzBuzzKata
{
    [TestClass]
    public class FizzBuzz
    {
        private NumberGenerator _fizBuzzNumberG;
        private List<ICondition> _iConditionList;
        [TestMethod]
        public void FizzBuzzPrinterDefine()
        {
            _fizBuzzNumberG = new NumberGenerator();
        }

        [TestMethod]
        public void PrintNumbersFromOneToTwenty()
        {
            _fizBuzzNumberG = new NumberGenerator();
            _iConditionList = new List<ICondition>();
            var numbersPrinted = _fizBuzzNumberG.ObtainNumbers(1,20,_iConditionList);
            Assert.AreEqual("12345678910111213141516171819",numbersPrinted);
        }
        [TestMethod]
        public void PrintMultipleOfThreeWithFizz()
        {
            _fizBuzzNumberG = new NumberGenerator();
            _iConditionList = new List<ICondition> {new MultipleThreeCondition()};
            var numbersPrinted = _fizBuzzNumberG.ObtainNumbers(1, 20,_iConditionList);
            Assert.AreEqual("12fizz45fizz78fizz1011fizz1314fizz1617fizz19", numbersPrinted);
        }

        [TestMethod]
        public void PrintMultipleOfFiveWithBuzz()
        {
            _fizBuzzNumberG = new NumberGenerator();
            _iConditionList = new List<ICondition> {new MultipleFiveCondition()};
            var numbersPrinted = _fizBuzzNumberG.ObtainNumbers(1, 20, _iConditionList);
            Assert.AreEqual("1234buzz6789buzz11121314buzz16171819", numbersPrinted);
        }

        [TestMethod]
        public void PrintMultipleOfFiveAndThreeWithBuzz()
        {
            _fizBuzzNumberG = new NumberGenerator();
            _iConditionList = new List<ICondition> {new MultipleFiveThreeCondition()};
            var numbersPrinted = _fizBuzzNumberG.ObtainNumbers(1, 20, _iConditionList);
            Assert.AreEqual("1234567891011121314fizzbuzz16171819", numbersPrinted);
        }

      
        [TestMethod]
        public void PrintFizzBuzz()
        {
            _fizBuzzNumberG = new NumberGenerator();
            _iConditionList = new List<ICondition>
            {
                new MultipleFiveThreeCondition(),
                new MultipleThreeCondition(),
                new MultipleFiveCondition()
            };
            var numbersPrinted = _fizBuzzNumberG.ObtainNumbers(1, 20, _iConditionList);
            Assert.AreEqual("12fizz4buzzfizz78fizzbuzz11fizz1314fizzbuzz1617fizz19", numbersPrinted);
        }

        [TestMethod]
        public void PrintNumberMultipleThreeJustOneNumber()
        {
            _fizBuzzNumberG = new NumberGenerator();
            _iConditionList = new List<ICondition>
            {
               new MultipleThreeCondition()
            };
            var numbersPrinted = _fizBuzzNumberG.ObtainNumbers(3, 4, _iConditionList);
            Assert.AreEqual("fizz", numbersPrinted);
        }
    }

    public class MultipleFiveThreeCondition : ICondition
    {
        public string Evaluate(int number)
        {
            string result = new MultipleThreeCondition().Evaluate(number) +
                            new MultipleFiveCondition().Evaluate(number);

            return result.Equals("fizzbuzz") ?
                result : number.ToString();
        }
    }

    public class MultipleFiveCondition : ICondition
    {
        public string Evaluate(int number)
        {
            return number % 5 == 0 ? "buzz" : number.ToString();
        }
    }

    public class MultipleThreeCondition : ICondition
    {
        public string Evaluate(int number)
        {
            return number%3 == 0 ? "fizz" : number.ToString();
        }
    }

    public interface ICondition
    {
        string Evaluate(int number);
    }


    public class NumberGenerator
    {
        
        public string ObtainNumbers(int index, int to, List<ICondition> iConditionList)
        {
            string printedNumberR = "";
            for (int i = index; i < to; i++)
            {
                string result = i.ToString();
                result = ApplyCondition(iConditionList, result, i);
                printedNumberR += result;

            }
            return printedNumberR;
        }

        private static string ApplyCondition(List<ICondition> iConditionList, string result, int i)
        {
            foreach (ICondition t in iConditionList)
            {
                result = t.Evaluate(i);
                if (result != i.ToString()) break;
            }
            return result;
        }
    }
}
