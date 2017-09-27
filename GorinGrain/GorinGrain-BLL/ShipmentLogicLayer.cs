using GorinGrain_BLL.ErrorHandling;
using GorinGrain_BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace GorinGrain_BLL
{
    public class ShipmentLogicLayer
    {
        //method to get top producer
        public long GetTopProducer(List<IShipmentInfoBO> myList)
        {
            long mostbushels = 0;
            long producerId = 0;
            try
            {
                //using LINQ functions to sort and group list data by producer, list brought in from logic layer
                IEnumerable<IGrouping<long, IShipmentInfoBO>> groupedList = myList.GroupBy(list => list.ProducerID);

                //for each Interface object in our list
                foreach (IGrouping<long, IShipmentInfoBO> producer in groupedList)
                {
                    // if the sum of all the QinB shipments from this producer is greater than the last producer's sum QinB 
                    if (producer.Sum(x => x.QuantityInBu) > mostbushels)
                    {
                        //producer ID will be highest value to check against
                        producerId = producer.Key;
                        mostbushels = producer.Sum(x => x.QuantityInBu);
                    }

                    else
                    {
                        //keep looping, mostbushels (top producer) does not change
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLogging.logError(e);
            }
            finally
            {
                //nothing needs to happen, error was caught if list did not poplulate
            }

            //this ID will be producer with larget number in QuantityInBu column
            return producerId;
        }
    }
}
