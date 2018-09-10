// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DebugRoutineUnitTest.cs" company="urb31075">
// All Right Reserved  
// </copyright>
// <summary>
//   Defines the DebugRoutineUnitTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PdfCombinerLibraryUnitTest
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Microsoft.Win32;

    using NUnit.Framework;
    using PdfCombinerLibrary;

    /// <summary>
    /// The debug routine unit test.
    /// </summary>
    [TestFixture]
    public class DebugRoutineUnitTest
    {
        /// <summary>
        /// The get data test.
        /// </summary>
        [Test, Category("DebugRoutine")]
        public void GetDataTest()
        {
            var result = new DebugRoutine().GetData(25);
            Assert.AreEqual(@"Input value is : 25", result);
        }
    }

    [TestFixture]
    public class DebugRoutineTheoryUnitTest
    {
        [Datapoint]
        public int Dupel = 100;

        [Datapoints]
        public IEnumerable<int> Dupel1 => Enumerable.Range(1, 4);

        [Datapoints]
        public IEnumerable<int> Dupel2 => Enumerable.Range(25, 3);

        [Theory]
        public void ValidSqrt(int inputData)
        {
            Assume.That(inputData > 2);
            var result = new DebugRoutine().Sqrt(inputData);
            Assert.That(result - 10, Is.Positive);
        }
    }

    [TestFixture]
    public class DebugRoutineParameterizedUnitTest
    {

        /// <summary>
        /// The get data test case source.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [Test, TestCaseSource(typeof(MyDataClass), nameof(MyDataClass.TestCases))]
        public string GetDataTestCaseSource(int value)
        {
            var result = new DebugRoutine().GetData(value);
            return result;
        }

        /// <summary>
        /// The my data class.
        /// </summary>
        public class MyDataClass
        {
            /// <summary>
            /// Gets the test cases.
            /// </summary>
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(1).Returns("Input value is : 1");
                    yield return new TestCaseData(2).Returns("Input value is : 2");
                    yield return new TestCaseData(3).Returns("Input value is : 3");
                }
            }
        }
    }

}
