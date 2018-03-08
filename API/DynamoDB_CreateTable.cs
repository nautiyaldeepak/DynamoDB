using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace DynamoDB_Create
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string AccessKey = "AKIAJ6MQDHUCYN7NPLVQ";
                string SecretKey = "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";
                Console.WriteLine("Enter Table Name ");
                string tableName = Console.ReadLine();
                var client = new AmazonDynamoDBClient(AccessKey, SecretKey, Amazon.RegionEndpoint.APSouth1);
                Console.WriteLine("Getting list of tables");
                List<string> currentTables = client.ListTables().TableNames;
                int count = currentTables.Count;
                for(int i =0;i<count;i++)
                {
                    Console.WriteLine(currentTables[i]);
                }

                Console.WriteLine("Enter Hash Key ");
                string HashKey = Console.ReadLine();
                Console.WriteLine("Enter Hash Key Type [N -> number, S -> String, B -> Binary]");
                string HashKeyType = Console.ReadLine();

                Console.WriteLine("Enter Range Key");
                string RangeKey = Console.ReadLine();
                Console.WriteLine("Enter Range Key Type [N -> number, S -> String, B -> Binary]");
                string RangeKeyType = Console.ReadLine();

                Console.WriteLine("Enter Read Capacity Unit");
                int ReadCapacity = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter Write Capacity Unit");
                int WriteCapacity = Convert.ToInt32(Console.ReadLine());

                if (!currentTables.Contains(tableName))
                {
                    var request = new CreateTableRequest
                    {
                        TableName = tableName,
                        AttributeDefinitions = new List<AttributeDefinition>
                        {
                            new AttributeDefinition
                            {
                                AttributeName = HashKey,
                                AttributeType = HashKeyType
                            },
                            new AttributeDefinition
                            {
                                AttributeName = RangeKey,
                                AttributeType = RangeKeyType
                            }
                        },
                        KeySchema = new List<KeySchemaElement>
                    {
                        new KeySchemaElement
                        {
                            AttributeName = HashKey,
                            KeyType = "HASH"
                        },
                        new KeySchemaElement
                        {
                            AttributeName = RangeKey,
                            KeyType = "RANGE"
                        }
                    },
                        ProvisionedThroughput = new ProvisionedThroughput
                        {
                            ReadCapacityUnits = ReadCapacity,
                            WriteCapacityUnits = WriteCapacity
                        },
                    };
                    var response = client.CreateTable(request);
                    Console.WriteLine("Table created with request ID: " + response.ResponseMetadata.RequestId);
                }
                else
                {
                    Console.WriteLine("Table Already Exists");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR Message: " + e.Message);
            }
            Console.ReadLine();
        }
    }
}
