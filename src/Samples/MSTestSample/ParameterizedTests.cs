namespace MSTestSample;

[TestClass]  
public class ParameterizedTests  
{  
    [DataTestMethod]  
    [DataRow(1, 1, 2)]
    [DataRow(2, 2, 4)]
    [DataRow(3, 3, 6)]
    [DataRow(4, 4, 8)]
    [DataRow(5, 5, 10)]
    public void Sum(int x, int y, int res)  
    {  
        Assert.AreEqual(res, x + y);  
    }  
}