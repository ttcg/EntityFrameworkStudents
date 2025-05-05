namespace Students.IntegrationTests
{
    public class BaseIntegrationTest
    {
        public BaseIntegrationTest(IntegrationTestFactory factory)
        {
            factory.ResetDatabase();
        }
    }
}
