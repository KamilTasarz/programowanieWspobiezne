namespace Data
{
    public abstract class LoggerApi
    {
        public static LoggerApi CreateApi()
        {
            string date = DateTime.Now.ToString("ddMMyyyy");
            string time = DateTime.Now.ToString("HHmmss");
            return new Logger($"BallsInfo_{date}_{time}.log");
        }

        public abstract void CreateLog(string message);
    }
}