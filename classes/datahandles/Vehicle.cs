using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes
{
    public class Vehicle : Script
    {
        Database db = new Database();

        private int id;
        private VehicleHash type;
        private Vector3 lastPosition;
        private Vector3 lastRotation;
        private NetHandle vehicle;
        private int colorA;
        private int colorB;

        /** Get/Set the last position of the vehicle. */
        public Vector3 LastPosition
        {
            set
            {
                lastPosition = value;
                updateVehiclePosition();
            }
            get
            {
                return lastPosition;
            }
        }

        /** Get/Set the last position of the vehicle. */
        public Vector3 LastRotation
        {
            set
            {
                lastRotation = value;
            }
            get
            {
                return lastRotation;
            }
        }

        /**  Get/Set the ID of the vehicle in the database. */
        public int ID
        {
            set
            {
                id = value;
            }
            get
            {
                return id;
            }
        }

        /** Get/Set the nethandle of the vehicle */
        public NetHandle Handle
        {
            set
            {
                vehicle = value;
            }
            get
            {
                return vehicle;
            }
        }

        /** Get/Set the vehicle hash type. */
        public VehicleHash Type
        {
            set
            {
                type = value;
            }
            get
            {
                return type;
            }
        }

        /** Get/Set the vehicle main color. */
        public int ColorA
        {
            set
            {
                colorA = value;
            }
            get
            {
                return colorA;
            }
        }

        /** Get/Set the vehicle secondary color. */
        public int ColorB
        {
            set
            {
                colorB = value;
            }
            get
            {
                return colorB;
            }
        }

        public Vehicle() { }

        public Vehicle(DataRow db)
        {
            ID = Convert.ToInt32(db["Id"]);
            Type = API.vehicleNameToModel(Convert.ToString(db["Type"]));
            LastPosition = new Vector3(Convert.ToSingle(db["X"]), Convert.ToSingle(db["Y"]), Convert.ToSingle(db["Z"]));
            LastRotation = new Vector3(Convert.ToSingle(db["RX"]), Convert.ToSingle(db["RY"]), Convert.ToSingle(db["RZ"]));
            ColorA = Convert.ToInt32(db["ColorA"]);
            ColorA = Convert.ToInt32(db["ColorB"]);
            Handle = API.createVehicle(Type, LastPosition, LastRotation, colorA, colorB);
        }

        /** Used internally to update the vehicles last position for when a player logs out. */
        private void updateVehiclePosition()
        {
            Vector3 vehPos = API.getEntityPosition(Handle);
            Vector3 vehRot = API.getEntityRotation(Handle);

            string before = "UPDATE Vehicles SET";
            string[] varNames = { "X", "Y", "Z", "RX", "RY", "RZ" };
            string after = string.Format("WHERE Id='{0}'", ID);
            object[] args = { vehPos.X, vehPos.Y, vehPos.Z, vehRot.X, vehRot.Y, vehRot.Z };
            db.compileQuery(before, after, varNames, args);
        }
    }
}
