using System;
using System.Collections.Generic;
using System.Linq;

namespace Incapsulation.Failures
{
    public class ReportMaker
    {
        public static List<string> FindDevicesFailedBeforeDateObsolete(
            int day,
            int month,
            int year,

            int[] failureTypes, 
            int[] deviceId, 
            object[][] times,
            List<Dictionary<string, object>> devices)
        {
            var date = new DateTime(year, month, day);
            var failures = new List<Failure>();

            for (int i = 0; i < failureTypes.Length; i++)
            {
                failures.Add(new Failure(
                    (FailureType)failureTypes[i],
                    new DateTime((int)times[i][2], (int)times[i][1], (int)times[i][0]),
                    new Device(deviceId[i], (string)devices[i]["Name"])));
            }
            return FindDevicesFailedBeforeDate(date, failures);
        }

        public static List<string> FindDevicesFailedBeforeDate(
            DateTime date, 
            List<Failure> failures)
        {
            return failures
                .Where(failure => failure.Date.CompareTo(date) < 0 && failure.IsSerious())
                .Select(failure => failure.Device.Name)
                .ToList();
        }
    }

    public enum FailureType
    {
        UnexpectedShutdown,
        ShortNonResponding,
        HardwareFailures,
        ConnectionProblems
    }

    public class Failure
    {
        public readonly FailureType Type;
        public readonly DateTime Date;
        public readonly Device Device;

        public Failure(FailureType type, DateTime date, Device device)
        {
            Type = type;
            Date = date;
            Device = device;
        }

        public bool IsSerious()
        {
            return Type == FailureType.UnexpectedShutdown
                || Type == FailureType.HardwareFailures;
        }
    }

    public class Device
    {
        public readonly int Id;
        public readonly string Name;

        public Device(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}