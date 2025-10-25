using FibonacciGenerator;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FibonacciGeneratorTests
{
    [TestFixture]
    public class FibbonacciTests
    {

        [TestCase(-1)]
        [TestCase(-1)]
        [TestCase(-100)]
        public void Generate_ShallThrowException_IfNIsSmallerThanZero(int n)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Fibonacci.Generate(n);
            });
        }

        [TestCase(47)]
        [TestCase(100)]
        [TestCase(1000)]
        public void Generate_ShallThrowException_IfNIsLargerThan46(int n)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Fibonacci.Generate(n);
            });
        }

        [Test]
        public void Generate_ShallProduceSequence_With_0_ForNEqualTo1()
        {
            var result = Fibonacci.Generate(1);
            Assert.AreEqual(new List<int> { 0 }, result);
        }

        [Test]
        public void Generate_ShallProduceSequence_With_0_1_ForNEqualTo2()
        {
            var result = Fibonacci.Generate(2);
            Assert.AreEqual(new List<int> { 0, 1 }, result);
        }

        [TestCase(3, new int[] { 0, 1, 1 })]
        [TestCase(5, new int[] { 0, 1, 1, 2,3 })]
        [TestCase(10, new int[] { 0, 1, 1, 2, 3,5,8,13,21,34})]
        public void Generate_ShallProduceValidForFibonacciSequence(int n, int[] expected)
        {
            var result = Fibonacci.Generate(n);
            Assert.AreEqual(expected, result);
        }
        [Test]
        public void Generate_ShallProduceSequenceWithLastNumber_1134903170_ForNEqualTo46()
        {
            var result = Fibonacci.Generate(46);
            const int FibonacciNumber46 = 1134903170;
            Assert.AreEqual(FibonacciNumber46, result.Last());
        }

        [Test]
        public void Generate_ShallProduceEmptySequence_ForNEqualTo0()
        {
            var result = Fibonacci.Generate(0);
            Assert.IsEmpty(result);
        }
    }
}
