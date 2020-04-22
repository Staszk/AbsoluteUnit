﻿using UnityEngine;

namespace AbsoluteUnit {
    public abstract partial class Verify
    {
        #region AreEqual

        #region Integers
        public static Result AreEqual(int expected, int actual, string message)
        {
            TestResult tr = expected == actual ? TestResult.Pass : TestResult.Fail;

            return new Result(message, tr);
        }

        public static Result AreEqual(int expected, int actual)
        {
            return AreEqual(expected, actual, string.Empty);
        }
        #endregion

        #region Floats
        public static Result AreEqual(float expected, float actual, float delta, string message)
        {
            float diff = Mathf.Abs(expected - actual);

            TestResult tr = diff <= delta ? TestResult.Pass : TestResult.Fail;

            return new Result(message, tr);
        }

        public static Result AreEqual(float expected, float actual, float delta)
        {
            return AreEqual(expected, actual, delta, string.Empty);
        }
        #endregion

        #region Objects
        public static Result AreEqual(object expected, object actual, string message)
        {
            TestResult tr = expected == actual ? TestResult.Pass : TestResult.Fail;

            return new Result(message, tr);
        }

        public static Result AreEqual(object expected, object actual)
        {
            return AreEqual(expected, actual, string.Empty);
        }
        #endregion

        #endregion
    }
}
