using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

namespace Tests
{

    public class LargeNumberTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void LargeNumberValueAddition()
        {

            LargeNumber ln = new LargeNumber(0, 0);
            LargeNumber ln2 = new LargeNumber(1.0f, 0);
            ln += ln2;
            Assert.That(Utils.AreFloatsEqual(1f, ln.value, 0.00000007f));//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueAdditionWithZeroExponent()
        {

            LargeNumber ln = new LargeNumber(0, 0);
            LargeNumber ln2 = new LargeNumber(1.0f, 6);
            ln += ln2;
            Assert.That(Utils.AreFloatsEqual(1f, ln.value, 0.00000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 6);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueAdditionWithSameExponent()
        {

            LargeNumber ln = new LargeNumber(3.21f, 6);
            LargeNumber ln2 = new LargeNumber(1.0f, 6);
            ln += ln2;
            Assert.That(Utils.AreFloatsEqual(4.21f, ln.value, 0.00000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 6);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueAdditionWithIncreasingExponent()
        {

            LargeNumber ln1 = new LargeNumber(9.0f, 6);
            LargeNumber ln2 = new LargeNumber(2.0f, 6);
            ln1 += ln2;

            Debug.Log("Inc" + ln1.value);
            Debug.Log("Inc" + ln1.exponent);

            Assert.That(Utils.AreFloatsEqual(1.1f, ln1.value, 0.00000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln1.exponent, 7);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueAdditionWithSmallerExponent()
        {

            LargeNumber ln = new LargeNumber(1.0f, 5);
            LargeNumber ln2 = new LargeNumber(1.0f, 6);
            ln += ln2;
            Debug.Log("Sm" + ln.value);
            Debug.Log("Sm" + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(1.1f, ln.value, 0.00000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 6);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueAdditionWithSmallerExponent2()
        {

            LargeNumber ln = new LargeNumber(1.0f, 5);
            LargeNumber ln2 = new LargeNumber(1.0f, 9);
            ln += ln2;
            Debug.Log("Sm" + ln.value);
            Debug.Log("Sm" + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(1.0001f, ln.value, 0.00000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 9);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueAdditionWithSmallerExponent3()
        {

            LargeNumber ln = new LargeNumber(1.0f, 15);
            LargeNumber ln2 = new LargeNumber(1.0f, 54);
            ln += ln2;
            Debug.Log("Sm" + ln.value);
            Debug.Log("Sm" + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(1, ln.value, 0.00000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 54);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueAdditionWithLargerExponent()
        {

            LargeNumber ln = new LargeNumber(1.0f, 6);
            LargeNumber ln2 = new LargeNumber(1.0f, 5);
            ln += ln2;
            Debug.Log("Lr" + ln.value);
            Debug.Log("Lr" + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(1.1f, ln.value, 0.00000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 6);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueAdditionWithLargerExponent2()
        {

            LargeNumber ln = new LargeNumber(1.0f, 9);
            LargeNumber ln2 = new LargeNumber(1.0f, 5);
            ln += ln2;
            Debug.Log("Lr" + ln.value);
            Debug.Log("Lr" + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(1.0001f, ln.value, 0.00000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 9);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueSubtractionWithLargerExponent3()
        {

            LargeNumber ln = new LargeNumber(1.0f, 42);
            LargeNumber ln2 = new LargeNumber(1.0f, 16);
            ln += ln2;
            Debug.Log("Lr" + ln.value);
            Debug.Log("Lr" + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(1f, ln.value, 0.000000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 42);//new LargeNumber(1.0f, 0), ln + ln2);
        }        // A Test behaves as an ordinary method
        [Test]
        public void LargeNumberValueSubtraction()
        {

            LargeNumber ln = new LargeNumber(5.0f, 0);
            LargeNumber ln2 = new LargeNumber(1.0f, 0);
            ln -= ln2;
            Debug.Log("value " + ln.value);
            Debug.Log("exp " + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(4f, ln.value, 0.00000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 0);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueSubtractionWithZeroExponent()
        {

            LargeNumber ln = new LargeNumber(0, 0);
            LargeNumber ln2 = new LargeNumber(1.0f, 6);
            ln -= ln2;
            Assert.That(Utils.AreFloatsEqual(1f, ln.value, 0.00000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, -6);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueSubtractionWithSameExponent()
        {

            LargeNumber ln = new LargeNumber(3.21f, 6);
            LargeNumber ln2 = new LargeNumber(1.0f, 6);
            ln -= ln2;
            Assert.That(Utils.AreFloatsEqual(2.21f, ln.value, 0.00000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 6);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueSubtractionWithDecreasingExponent()
        {

            LargeNumber ln1 = new LargeNumber(1f, 6);
            LargeNumber ln2 = new LargeNumber(9.0f, 5);
            ln1 -= ln2;

            Debug.Log("Inc" + ln1.value);
            Debug.Log("Inc" + ln1.exponent);

            Assert.That(Utils.AreFloatsEqual(1f, ln1.value, 0.00000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln1.exponent, 5);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberNEWFUN()
        {

            LargeNumber ln1 = new LargeNumber(5, 6);
            //ln1 -= ln2;
            ln1 = ln1 * 3f;

            Debug.Log("Inc" + ln1.value);
            Debug.Log("Inc" + ln1.exponent);
            Assert.That(Utils.AreFloatsEqual(1.5f, ln1.value, 0.00007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln1.exponent, 7);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueSubtractionWithSmallerExponent()
        {

            LargeNumber ln = new LargeNumber(4.22f, 5);
            LargeNumber ln2 = new LargeNumber(3.12f, 6);
            ln -= ln2;
            Debug.Log("Sm" + ln.value);
            Debug.Log("Sm" + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(2.698f, ln.value, 0.00000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 6);//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.minus, true);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueSubtractionWithSmallerExponent2()
        {

            LargeNumber ln = new LargeNumber(1.0f, 5);
            LargeNumber ln2 = new LargeNumber(1.0f, 9);
            ln += ln2;
            Debug.Log("Sm" + ln.value);
            Debug.Log("Sm" + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(1.0001f, ln.value, 0.00000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 9);//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.minus, true);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueSubtractionWithSmallerExponent3()
        {

            LargeNumber ln = new LargeNumber(1.0f, 15);
            LargeNumber ln2 = new LargeNumber(1.0f, 54);
            ln += ln2;
            Debug.Log("Sm" + ln.value);
            Debug.Log("Sm" + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(1, ln.value, 0.00000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 54);//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.minus, true);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueSubtractionWithLargerExponent()
        {

            LargeNumber ln = new LargeNumber(1.0f, 6);
            LargeNumber ln2 = new LargeNumber(1.0f, 5);
            ln += ln2;
            Debug.Log("Lr" + ln.value);
            Debug.Log("Lr" + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(1.1f, ln.value, 0.00000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 6);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueSubtractionWithLargerExponent2()
        {

            LargeNumber ln = new LargeNumber(1.0f, 9);
            LargeNumber ln2 = new LargeNumber(1.0f, 5);
            ln += ln2;
            Debug.Log("Lr" + ln.value);
            Debug.Log("Lr" + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(1.0001f, ln.value, 0.00000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 9);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueAdditionWithLargerExponent3()
        {

            LargeNumber ln = new LargeNumber(1.0f, 42);
            LargeNumber ln2 = new LargeNumber(1.0f, 16);
            ln += ln2;
            Debug.Log("Lr" + ln.value);
            Debug.Log("Lr" + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(1f, ln.value, 0.000000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 42);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueMultiplicationZeroExponent()
        {

            LargeNumber ln = new LargeNumber(2.0f, 0);
            LargeNumber ln2 = new LargeNumber(3.0f, 0);
            ln *= ln2;
            Debug.Log("MZ" + ln.value);
            Debug.Log("MZ" + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(6f, ln.value, 0.000000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 0);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueMultiplicationZeroExponent1()
        {

            LargeNumber ln = new LargeNumber(2.0f, 0);
            LargeNumber ln2 = new LargeNumber(3.0f, 4);
            ln *= ln2;
            Debug.Log("MZ" + ln.value);
            Debug.Log("MZ" + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(6f, ln.value, 0.000000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 4);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueMultiplicationZeroExponent2()
        {

            LargeNumber ln = new LargeNumber(2.0f, 7);
            LargeNumber ln2 = new LargeNumber(3.0f, 0);
            ln *= ln2;
            Debug.Log("MZ" + ln.value);
            Debug.Log("MZ" + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(6f, ln.value, 0.000000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 7);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueMultiplicationIncreasingExponent()
        {

            LargeNumber ln = new LargeNumber(8.0f, 0);
            LargeNumber ln2 = new LargeNumber(3.0f, 0);
            ln *= ln2;
            Debug.Log("MI" + ln.value);
            Debug.Log("MI   " + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(2.4f, ln.value, 0.000000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 1);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueMultiplicationIncreasingExponent2()
        {

            LargeNumber ln = new LargeNumber(8.0f, 0);
            ln *= 23;
            Debug.Log("MI" + ln.value);
            Debug.Log("MI   " + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(1.84f, ln.value, 0.000000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 2);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueMultiplicationLargerExponent()
        {

            LargeNumber ln = new LargeNumber(4.0f, 5);
            LargeNumber ln2 = new LargeNumber(2.0f, 3);
            ln *= ln2;
            Debug.Log("MLr" + ln.value);
            Debug.Log("MLr" + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(8f, ln.value, 0.000000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 8);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueMultiplicationSmallerExponent()
        {

            LargeNumber ln = new LargeNumber(6.0f, 2);
            LargeNumber ln2 = new LargeNumber(2.0f, 50);
            ln *= ln2;
            Debug.Log("Msm" + ln.value);
            Debug.Log("Msm" + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(1.2f, ln.value, 0.000000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 53);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueDivisionZeroExponent()
        {

            LargeNumber ln = new LargeNumber(2.0f, 0);
            LargeNumber ln2 = new LargeNumber(3.0f, 0);
            ln /= ln2;
            Debug.Log("MZ" + ln.value);
            Debug.Log("MZ" + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(6.666667f, ln.value, 0.000000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, -1);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueDivisionZeroExponent1()
        {

            LargeNumber ln = new LargeNumber(2.0f, 0);
            LargeNumber ln2 = new LargeNumber(3.0f, 4);
            ln /= ln2;
            Debug.Log("MZ" + ln.value);
            Debug.Log("MZ" + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(6.666667f, ln.value, 0.000000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, -5);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueDivisionZeroExponent2()
        {

            LargeNumber ln = new LargeNumber(2.0f, 7);
            LargeNumber ln2 = new LargeNumber(3.0f, 0);
            ln /= ln2;
            Debug.Log("MZ" + ln.value);
            Debug.Log("MZ" + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(6.66666667f, ln.value, 0.0000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 6);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueDivisionIncreasingExponent()
        {

            LargeNumber ln = new LargeNumber(8.0f, 13);
            LargeNumber ln2 = new LargeNumber(4.0f, 5);
            ln /= ln2;
            Debug.Log("MI" + ln.value);
            Debug.Log("MI   " + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(2f, ln.value, 0.000000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 8);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueDivisionIncreasingExponent2()
        {

            LargeNumber ln = new LargeNumber(8.0f, 3);
            ln /= 23;
            Debug.Log("MI" + ln.value);
            Debug.Log("MI   " + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(3.47826086957f, ln.value, 0.000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, 2);//new LargeNumber(1.0f, 0), ln + ln2);
            //Assert.AreEqual(ln.minus, );//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueDivisionLargerExponent()
        {

            LargeNumber ln = new LargeNumber(4.0f, 5);
            LargeNumber ln2 = new LargeNumber(2.0f, 9);
            ln /= ln2;
            Debug.Log("MLr" + ln.value);
            Debug.Log("MLr" + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(2f, ln.value, 0.000000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, -4);//new LargeNumber(1.0f, 0), ln + ln2);
        }
        [Test]
        public void LargeNumberValueDivisionSmallerExponent()
        {

            LargeNumber ln = new LargeNumber(6.0f, 2);
            LargeNumber ln2 = new LargeNumber(2.0f, 50);
            ln /= ln2;
            Debug.Log("Msm" + ln.value);
            Debug.Log("Msm" + ln.exponent);
            Assert.That(Utils.AreFloatsEqual(3f, ln.value, 0.000000007f));//new LargeNumber(1.0f, 0), ln + ln2);
            Assert.AreEqual(ln.exponent, -48);//new LargeNumber(1.0f, 0), ln + ln2);
        }
    }

}

