using System;
using Xunit;
using System.Net;
using System.Threading.Tasks;
using DemoAPI.model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
using Moq;
using DemoAPI.Controllers;
public class Test
{
    /*
    [Fact]
    public async Task Get_Test()
    {
        var client=new ProviderTest().client;
        var response=await client.GetAsync("api/DynamoDB/RetriveBook?id=555");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }

    const string skip = "Class X disabled";

 [Fact(Skip=skip)]
    public async Task Post_Test()
    {
        var client=new ProviderTest().client;
        var response=await client.PostAsync("api/DynamoDB/CreateBook",new StringContent(JsonConvert.SerializeObject(BookData()),Encoding.UTF8,"application/json"));
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }

    public static Book BookData()
    {
        Book model=new Book();
        model.Id=10;
        model.ISBN="46737373";
        model.PageCount=200;
        model.Price=88;
        model.QuantityOnHand=6;
        model.Title="TestbyXunit";
        model.Dimensions="9289372";
        return model;
    }

    [Theory]
     [MemberData(nameof(Data))]
    public async Task Get_Test_Theory(int id)
    {
        var client=new ProviderTest().client;
        var response=await client.GetAsync("api/DynamoDB/RetriveBook?id="+id);
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }

     [Theory]
     [ClassData(typeof(IDTestData))]
    public async Task Get_Test_ClassData(int id)
    {
        var client=new ProviderTest().client;
        var response=await client.GetAsync("api/DynamoDB/RetriveBook?id="+id);
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }*/
    [Fact]
    public void TestMoq()  
        {  
            var mock = new Mock<IBookService>();  
            mock.Setup(p => p.Add(10,5)).Returns(15);  
            DynamoDBController home = new DynamoDBController(mock.Object);  
            int result =  home.Add(10,5);  
            Assert.Equal(15, result);  
        }

    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { 10 },
            new object[] {9 },
            new object[] { 555  },
            new object[] { 556},
        };

}
public class IDTestData : TheoryData<int>
{
    public IDTestData()
    {
        Add(10);
        Add(9);
        Add(555);
  
    }
}

