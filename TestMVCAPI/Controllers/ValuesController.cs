using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestMVCAPI.Models.Fees;
using System.Net;
using Newtonsoft.Json;
using TestMVCAPI.Models.Orders;
using TestMVCAPI.Models;

namespace TestMVCAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        List<FeesResponse> fees = new List<FeesResponse>()
            {
                new FeesResponse
                {
                    order_item_type = "Real Property Recording",
                    fees = new List<Fee>
                    {
                         new Fee()
                         {
                             amount = "26.00",
                             name = "Recording (first page)",
                             type = "flat"
                         },
                         new Fee()
                         {
                             amount = "1.00",
                             name = "Recording (additional pages)",
                             type = "per-page"
                         }
                    },
                    distributions = new List<Distribution>
                    {
                        new Distribution()
                        {
                            name = "Recording Fee",
                            amount = "5.00"
                        },
                        new Distribution()
                        {
                            name = "Records Management and Preservation Fee",
                            amount = "10.00"
                        },
                        new Distribution()
                        {
                            name = "Records Archive Fee",
                            amount = "10.00"
                        },
                        new Distribution()
                        {
                            name = "Courthouse Security",
                            amount = "1.00"
                        }
                    }
                },
                new FeesResponse
                {
                    order_item_type = "Birth Certificate",
                    fees = new List<Fee>
                    {
                        new Fee()
                        {
                            amount = "23.00",
                            name = "Birth Certificate Fees",
                            type = "flat"
                        }
                    },
                    distributions = new List<Distribution>
                    {
                        new Distribution()
                        {
                            name = "County Clerk Fee",
                            amount = "20.00"
                        },
                        new Distribution()
                        {
                            name = "Records Management and Preservation Fee",
                            amount = "10.00"
                        },
                        new Distribution()
                        {
                            name = "Vital Statistics Fee",
                            amount = "1.00"
                        },
                        new Distribution()
                        {
                            name = "Vital Statistics Preservation Fee",
                            amount = "1.00"
                        }
                    }
                }
            };

        List<OrdersResponse> orders = new List<OrdersResponse>()
        {
            new OrdersResponse()
            {
                order_date = "1/11/2015",
                order_number = "20150111000001",
                order_items = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        order_item_id = 1,
                        type = "Real Property Recording",
                        pages = 3
                    },
                    new OrderItem()
                    {
                        order_item_id = 2,
                        type = "Real Property Recording",
                        pages = 1
                    }
                }
            },
            new OrdersResponse()
            {
                order_date = "1/17/2015",
                order_number = "20150111000001",
                order_items = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        order_item_id = 3,
                        type = "Real Property Recording",
                        pages = 2
                    },
                    new OrderItem()
                    {
                        order_item_id = 4,
                        type = "Real Property Recording",
                        pages = 20
                    }
                }
            },
            new OrdersResponse()
            {
                order_date = "1/18/2015",
                order_number = "20150118000001",
                order_items = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        order_item_id = 5,
                        type = "Real Property Recording",
                        pages = 5
                    },
                    new OrderItem()
                    {
                        order_item_id = 6,
                        type = "Birth Certificate",
                        pages = 1
                    }
                }
            },
            new OrdersResponse()
            {
                order_date = "1/23/2015",
                order_number = "20150123000001",
                order_items = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        order_item_id = 7,
                        type = "Birth Certificate",
                        pages = 1
                    },
                    new OrderItem()
                    {
                        order_item_id = 8,
                        type = "Birth Certificate",
                        pages = 1
                    }
                }
            }
        };


        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var req = new List<FeesResponse>()
            {
                new FeesResponse
                {
                    order_item_type = "aaa",
                    distributions = new List<Distribution>
                    {
                        new Distribution()
                        {
                            amount = "23",
                            name = "bb"
                        }
                    },
                    fees = new List<Fee>
                    {
                         new Fee()
                         {
                             amount = "123",
                             name = "a",
                             type = "ab"
                         }
                    }
                }
            };

            var res = req.FirstOrDefault();

            return new ObjectResult(res);
        }

        // GET api/values/5
        [HttpGet("{value}")]
        public ActionResult Get(string value)
        {
            object orderNumber = null;
            List<string> orderType = new List<string>();
            foreach (OrdersResponse order in orders)
            {
                if(order.order_number == value)
                {
                    orderNumber = order.order_number;
                    for(int i = 0; i < order.order_items.Count; i++)
                    {
                        orderType.Add(order.order_items[i].type);
                    }
                }
            }
            float sumAmount = 0;
            List<string> amount = new List<string>();
            for (var i = 0; i < orderType.Count; i++)
            {
                foreach (var fee in fees)
                {
                    var type = orderType[i];
                    
                    if(type == fee.order_item_type)
                    {
                        for(int y = 0; y < fee.fees.Count; y++)
                        {
                            amount.Add(fee.fees[y].amount);
                        }
                    }
                }
            }
            for (int z = 0; z < amount.Count; z++)
            {
                sumAmount += float.Parse(amount[z]);
            }

            return Json(new OrderMap() {
                OrderName = orderType,
                OrderPrice = amount,
                TotalAmount = sumAmount.ToString()
            });
        }

        [Route("getList")]
        [HttpGet("ordersArray")]
        public ActionResult GetList([FromQuery]List<string> ordersArray)
        {
            object orderNumber = null;
            List<string> orderType = new List<string>();
            float sumAmount = 0;
            List<string> distributionAmount = new List<string>();

            for (var a = 0; a < ordersArray.Count; a++)
            {
                foreach (OrdersResponse order in orders)
                {
                    if (ordersArray[a] == order.order_number)
                    {
                        orderNumber = order.order_number;
                        for (int i = 0; i < order.order_items.Count; i++)
                        {
                            orderType.Add(order.order_items[i].type);
                        }
                    }
                }
                
                for (var i = 0; i < orderType.Count; i++)
                {
                    foreach (var fee in fees)
                    {
                        var type = orderType[i];

                        if (type == fee.order_item_type)
                        {
                            for (int y = 0; y < fee.distributions.Count; y++)
                            {
                                distributionAmount.Add(fee.distributions[y].amount);
                            }
                        }
                    }
                }
                for (int z = 0; z < distributionAmount.Count; z++)
                {
                    sumAmount += float.Parse(distributionAmount[z]);
                }
            }
            
            return Json(new List<DistrubitionMap>()
            {
                new DistrubitionMap()
                {
                    OrderName = orderType,
                    DistributionPrice = distributionAmount,
                    TotalAmount = sumAmount.ToString()
                }
            });
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
