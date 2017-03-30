using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using MicrOrm.Core;
using Npgsql;
using NUnit.Framework;

namespace MicrOrm.Test
{
    public class PerformanceTest
    {

        [Test]
        public void TestLargeQueryRaw()
        {
            var query =
                @"SELECT
                events.id,
                events.asset_id,        
                events.container_asset_id, 
                events.event_time, 
                events.received_time,
                ST_X(events.position) position_lon, 
                ST_Y(events.position) position_lat,   
                events.position_error,
                event_types.name event_type_name
                FROM events
                JOIN event_types ON event_types.id = events.event_type_id
                WHERE events.container_asset_id = 79
                AND events.event_time >= '1/01/2016 09:38:00'::timestamp without time zone
                    AND events.event_time <= '4/5/2017 09:38:00'::timestamp without time zone
                    AND NOT(ST_X(events.position) = 0.0 AND ST_Y(events.position) = 0.0)
                AND events.is_historic; ";


            var sw = new Stopwatch();
            sw.Start();

            var result = new List<VehicleHistoryReportModel>();
            using (var conn = new NpgsqlConnection("host=odb.logikos.com;port=21000;database=protechs;username=ptuser;password=pt0917;ConvertInfinityDateTime=true"))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(NpgsqlModelFromRecord(reader));
                        }
                    }
                }
            }

            sw.Stop();

            Console.WriteLine("{0} records, {1}ms", result.Count, sw.ElapsedMilliseconds);

            Assert.Greater(result.Count, 0);
        }


        [Test]
        public void TestLargeQuery()
        {
            var connection = new MicrOrmConnectionProvider(
                "host=odb.logikos.com;port=21000;database=protechs;username=ptuser;password=pt0917;ConvertInfinityDateTime=true",
                "Npgsql.NpgsqlFactory, Npgsql");

            var query =
                @"SELECT
                events.id,
                events.asset_id,        
                events.container_asset_id, 
                events.event_time, 
                events.received_time,
                ST_X(events.position) position_lon, 
                ST_Y(events.position) position_lat,   
                events.position_error,
                event_types.name event_type_name
                FROM events
                JOIN event_types ON event_types.id = events.event_type_id
                WHERE events.container_asset_id = 79
                AND events.event_time >= '1/01/2016 09:38:00'::timestamp without time zone
                    AND events.event_time <= '4/5/2017 09:38:00'::timestamp without time zone
                    AND NOT(ST_X(events.position) = 0.0 AND ST_Y(events.position) = 0.0)
                AND events.is_historic; ";


            var sw = new Stopwatch();
            sw.Start();

            var result = new List<VehicleHistoryReportModel>();
            using (var db = connection.Database)
            {
                result = db.ExecuteReader(query).Select(ModelFromRecord).ToList();
            }

            sw.Stop();

            Console.WriteLine("{0} records, {1}ms", result.Count, sw.ElapsedMilliseconds);

            Assert.Greater(result.Count, 0);
        }


        protected VehicleHistoryReportModel ModelFromRecord(IDataRecord record)
        {
            var model = new VehicleHistoryReportModel()
            {
                Id = (int) record["id"],
                AssetId = (int) record["asset_id"],
                ContainerAssetId = new List<int> {(int) record["container_asset_id"]},
                EventTime = new DateTime(((DateTime) record["event_time"]).Ticks, DateTimeKind.Utc),
                ReceivedTime = new DateTime(((DateTime) record["received_time"]).Ticks, DateTimeKind.Utc),
                Longitude = (double) record["position_lon"],
                Latitude = (double) record["position_lat"],
                PositionError = (int) record["position_error"],
                EventTypeName = (string) record["event_type_name"]
            };
            return model;
        }

        protected VehicleHistoryReportModel NpgsqlModelFromRecord(NpgsqlDataReader record)
        {
            var model = new VehicleHistoryReportModel()
            {
                Id = (int) record["id"],
                AssetId = (int) record["asset_id"],
                ContainerAssetId = new List<int> {(int) record["container_asset_id"]},
                EventTime = new DateTime(((DateTime) record["event_time"]).Ticks, DateTimeKind.Utc),
                ReceivedTime = new DateTime(((DateTime) record["received_time"]).Ticks, DateTimeKind.Utc),
                Longitude = (double) record["position_lon"],
                Latitude = (double) record["position_lat"],
                PositionError = (int) record["position_error"],
                EventTypeName = (string) record["event_type_name"]
            };
            return model;
        }


        public class VehicleHistoryReportModel
        {
            public int Id { get; set; }
            public int AssetId { get; set; }
            public List<int> ContainerAssetId { get; set; }
            public DateTime EventTime { get; set; }
            public DateTime ReceivedTime { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public int PositionError { get; set; }
            public string EventTypeName { get; set; }
        }
    }
}