using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using DemoAPI.model;
namespace DemoAPI.Controllers
{
    [ApiController]
   
    public class DynamoDBController : ControllerBase
    {
        private IBookService _service;
        public DynamoDBController(IBookService service)
        {
            this._service=service;
        }
        private static AmazonDynamoDBClient client=new AmazonDynamoDBClient("AKIAXMJHA33LBVFKALPF","Vn/C7Cqiy9XhRTIIM7sEli69VKM4eTdbSVAaTckt",RegionEndpoint.USEast2);
       private static string tableName = "ProductCatalog";
        // The sample uses the following id PK value to add bsook item.
        private static int sampleBookId = 555;
        
        [HttpPost]
        [Route("api/DynamoDB/CreateBook")]
        public async Task<IActionResult> CreateBook([FromBody]Book model)
        {
            Table productCatalog = Table.LoadTable(client, tableName);
            try
            {
                var book = new Document();
                book["Id"] = model.Id;
                book["Title"] = "Book " + model.Id;
                book["Price"] = model.Price;
                book["ISBN"] = model.ISBN;               
                book["PageCount"] = model.PageCount;
                book["Dimensions"] = model.Dimensions;
                book["InPublication"] = new DynamoDBBool(true);
                book["InStock"] = new DynamoDBBool(false);
                book["QuantityOnHand"] = model.QuantityOnHand;

                await productCatalog.PutItemAsync(book);
                return Ok();
            }
            catch(Exception)
            {
                return BadRequest();
            }
            
        }
        [HttpGet]
        [Route("api/DynamoDB/RetriveBook")]
        public async Task<IActionResult> RetriveBook(int id)
        {

            
             Table productCatalog = Table.LoadTable(client, tableName);
            try
            {
                
                    GetItemOperationConfig config = new GetItemOperationConfig
                    {
                        AttributesToGet = new List<string> { "Id", "ISBN", "Title", "Authors", "Price" },
                        ConsistentRead = true
                    };
                    Document document =await productCatalog.GetItemAsync(id);
                    Book model=new Book();
                    foreach (var attribute in document.GetAttributeNames())
                     {
                           string stringValue = null;
                           var value = document[attribute];
                           if (value is Primitive)
                               stringValue = value.AsPrimitive().Value.ToString();
                                 else if (value is PrimitiveList)
                                stringValue = string.Join(",", (from primitive
                                    in value.AsPrimitiveList().Entries
                                                    select primitive.Value).ToArray());
                                         Console.WriteLine("{0} - {1}", attribute, stringValue);
                     }
                    // Document document =await _service.GetById(id);
                    return Ok(document.ToList());
            } 
            catch(Exception)
            {
                return BadRequest();
            }
           
        }
    public int Add(int a,int b){
        return _service.Add(a,b);
    }
    }
}