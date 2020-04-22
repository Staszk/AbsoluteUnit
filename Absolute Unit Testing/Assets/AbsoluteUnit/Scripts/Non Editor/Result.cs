
namespace AbsoluteUnit {
    public enum TestResult { Pass, Fail, Warning, Inconclusive }

    public class Result
    {
        public string message;
        public TestResult testResult;

        public Result(string message, TestResult testResult)
        {
            this.message = message;
            this.testResult = testResult;
        }
    }
}
