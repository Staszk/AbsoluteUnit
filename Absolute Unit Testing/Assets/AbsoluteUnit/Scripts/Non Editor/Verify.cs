namespace AbsoluteUnit
{
    public abstract partial class Verify
    {
        #region Pass
        public static Result Pass(string message)
        {
            return new Result(message, TestResult.Pass);
        }

        public static Result Pass()
        {
            return Pass(string.Empty);
        }
        #endregion

        #region Fail
        public static Result Fail(string message)
        {
            return new Result(message, TestResult.Fail);
        }

        public static Result Fail()
        {
            return Fail(string.Empty);
        }
        #endregion

        #region Warning
        public static Result Warning(string message)
        {
            return new Result(message, TestResult.Warning);
        }

        public static Result Warning()
        {
            return Warning(string.Empty);
        }
        #endregion

        #region Inconclusive
        public static Result Inconclusive(string message)
        {
            return new Result(message, TestResult.Inconclusive);
        }

        public static Result Inconclusive()
        {
            return Inconclusive(string.Empty);
        }
        #endregion
    }
}
