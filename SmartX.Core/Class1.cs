using System;
using System.Collections.Generic;

namespace SmartX.Core
{
    // Generics: Handles disparate incoming data structures uniformly without type-coercion overhead.
    public class TelemetryPacket<T>
    {
        public string MacAddress { get; set; }
        public string Location { get; set; }
        public T Payload { get; set; }

        // Operator Overloading: Allows direct aggregation of sensor values.
        public static double operator +(TelemetryPacket<T> a, TelemetryPacket<T> b)
        {
            return Convert.ToDouble(a.Payload) + Convert.ToDouble(b.Payload);
        }
    }

    public class DeploymentNode
    {
        public string ZoneName { get; set; }
        public List<DeploymentNode> SubZones { get; set; } = new List<DeploymentNode>();

        // Recursion: Parses nested device deployment trees to validate a node's configuration.
        public bool ValidateHierarchy(string targetZone)
        {
            if (ZoneName == targetZone) return true;

            foreach (var child in SubZones)
            {
                if (child.ValidateHierarchy(targetZone)) return true;
            }
            return false;
        }
    }
}