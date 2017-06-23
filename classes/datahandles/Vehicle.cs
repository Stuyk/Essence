using Essence.classes.utility;
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
        private Tuple<int, int, int> primaryColor;
        private Tuple<int, int, int> secondaryColor;
        private int spoiler = 0;
        private int frontBumper = 0;
        private int rearBumper = 0;
        private int sideSkirt = 0;
        private int exhaust = 0;
        private int frame = 0;
        private int grille = 0;
        private int hood = 0;
        private int fender = 0;
        private int rightFender = 0;
        private int roof = 0;
        private int engine = 0;
        private int brakes = 0;
        private int transmission = 0;
        private int horns = 0;
        private int suspension = 0;
        private int armor = 0;
        private int turbo = 0;
        private int xenon = 0;
        private int frontWheels = 0;
        private int backWheels = 0;
        private int plateHolders = 0;
        private int trimDesign = 0;
        private int ornaments = 0;
        private int dialDesign = 0;
        private int steeringWheel = 0;
        private int shiftLever = 0;
        private int plaques = 0;
        private int hydraulics = 0;
        private int livery = 0;
        private int plate = 0;
        private int windowTint = 0;

        /** Get/Set the last position of the vehicle. */
        public Vector3 LastPosition
        {
            set
            {
                lastPosition = value;
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

        public Tuple<int, int, int> PrimaryColor
        {
            set
            {
                primaryColor = value;
            }
            get
            {
                return primaryColor;
            }
        }

        public Tuple<int, int, int> SecondaryColor
        {
            set
            {
                secondaryColor = value;
            }
            get
            {
                return secondaryColor;
            }
        }

        public int Spoiler
        {
            set
            {
                spoiler = value;
            }
            get
            {
                return spoiler;
            }
        }

        public int FrontBumper
        {
            set
            {
                frontBumper = value;
            }
            get
            {
                return frontBumper;
            }
        }

        public int RearBumper
        {
            set
            {
                rearBumper = value;
            }
            get
            {
                return rearBumper;
            }
        }

        public int SideSkirt
        {
            set
            {
                sideSkirt = value;
            }
            get
            {
                return sideSkirt;
            }
        }

        public int Exhaust
        {
            set
            {
                exhaust = value;
            }
            get
            {
                return exhaust;
            }
        }

        public int Frame
        {
            set
            {
                frame = value;
            }
            get
            {
                return frame;
            }
        }

        public int Grille
        {
            set
            {
                grille = value;
            }
            get
            {
                return grille;
            }
        }

        public int Hood
        {
            set
            {
                hood = value;
            }
            get
            {
                return hood;
            }
        }

        public int Fender
        {
            set
            {
                fender = value;
            }
            get
            {
                return fender;
            }
        }

        public int RightFender
        {
            set
            {
                rightFender = value;
            }
            get
            {
                return rightFender;
            }
        }

        public int Roof
        {
            set
            {
                roof = value;
            }
            get
            {
                return roof;
            }
        }

        public int Engine
        {
            set
            {
                engine = value;
            }
            get
            {
                return engine;
            }
        }

        public int Brakes
        {
            set
            {
                brakes = value;
            }
            get
            {
                return brakes;
            }
        }

        public int Transmission
        {
            set
            {
                transmission = value;
            }
            get
            {
                return transmission;
            }
        }

        public int Horns
        {
            set
            {
                horns = value;
            }
            get
            {
                return horns;
            }
        }

        public int Suspension
        {
            set
            {
                suspension = value;
            }
            get
            {
                return suspension;
            }
        }

        public int Armor
        {
            set
            {
                armor = value;
            }
            get
            {
                return armor;
            }
        }

        public int Turbo
        {
            set
            {
                turbo = value;
            }
            get
            {
                return turbo;
            }
        }


        public int Xenon
        {
            set
            {
                xenon = value;
            }
            get
            {
                return xenon;
            }
        }

        public int FrontWheels
        {
            set
            {
                frontWheels = value;
            }
            get
            {
                return frontWheels;
            }
        }

        public int BackWheels
        {
            set
            {
                backWheels = value;
            }
            get
            {
                return backWheels;
            }
        }

        public int PlateHolders
        {
            set
            {
                plateHolders = value;
            }
            get
            {
                return plateHolders;
            }
        }

        public int TrimDesign
        {
            set
            {
                trimDesign = value;
            }
            get
            {
                return trimDesign;
            }
        }

        public int Ornaments
        {
            set
            {
                ornaments = value;
            }
            get
            {
                return ornaments;
            }
        }

        public int DialDesign
        {
            set
            {
                dialDesign = value;
            }
            get
            {
                return dialDesign;
            }
        }

        public int SteeringWheel
        {
            set
            {
                steeringWheel = value;
            }
            get
            {
                return steeringWheel;
            }
        }

        public int ShiftLever
        {
            set
            {
                shiftLever = value;
            }
            get
            {
                return shiftLever;
            }
        }

        public int Plaques
        {
            set
            {
                plaques = value;
            }
            get
            {
                return plaques;
            }
        }

        public int Hydraulics
        {
            set
            {
                hydraulics = value;
            }
            get
            {
                return hydraulics;
            }
        }

        public int Livery
        {
            set
            {
                livery = value;
            }
            get
            {
                return livery;
            }
        }

        public int Plate
        {
            set
            {
                plate = value;
            }
            get
            {
                return plate;
            }
        }

        public int WindowTint
        {
            set
            {
                windowTint = value;
            }
            get
            {
                return windowTint;
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
            API.setVehicleEngineStatus(Handle, false);
            loadVehicleModsFromTable(db);
            updateVehicleMods();
        }

        private void loadVehicleModsFromTable(DataRow db)
        {
            PrimaryColor = Tuple.Create(Convert.ToInt32(db["PrimaryR"]), Convert.ToInt32(db["PrimaryG"]), Convert.ToInt32(db["PrimaryB"]));
            SecondaryColor = Tuple.Create(Convert.ToInt32(db["SecondaryR"]), Convert.ToInt32(db["SecondaryG"]), Convert.ToInt32(db["SecondaryB"]));
            Spoiler = Convert.ToInt32(db["Spoiler"]);
            FrontBumper = Convert.ToInt32(db["FrontBumper"]);
            RearBumper = Convert.ToInt32(db["RearBumper"]);
            SideSkirt = Convert.ToInt32(db["SideSkirt"]);
            Exhaust = Convert.ToInt32(db["Exhaust"]);
            Frame = Convert.ToInt32(db["Frame"]);
            Grille = Convert.ToInt32(db["Grille"]);
            Hood = Convert.ToInt32(db["Hood"]);
            Fender = Convert.ToInt32(db["Fender"]);
            RightFender = Convert.ToInt32(db["RightFender"]);
            Roof = Convert.ToInt32(db["Roof"]);
            Engine = Convert.ToInt32(db["Engine"]);
            Brakes = Convert.ToInt32(db["Brakes"]);
            Transmission = Convert.ToInt32(db["Transmission"]);
            Horns = Convert.ToInt32(db["Horns"]);
            Suspension = Convert.ToInt32(db["Suspension"]);
            Armor = Convert.ToInt32(db["Armor"]);
            Xenon = Convert.ToInt32(db["Xenon"]);
            FrontWheels = Convert.ToInt32(db["FrontWheels"]);
            BackWheels = Convert.ToInt32(db["BackWheels"]);
            PlateHolders = Convert.ToInt32(db["PlateHolders"]);
            TrimDesign = Convert.ToInt32(db["TrimDesign"]);
            Ornaments = Convert.ToInt32(db["Ornaments"]);
            DialDesign = Convert.ToInt32(db["DialDesign"]);
            SteeringWheel = Convert.ToInt32(db["SteeringWheel"]);
            ShiftLever = Convert.ToInt32(db["ShiftLever"]);
            Plaques = Convert.ToInt32(db["Plaques"]);
            Hydraulics = Convert.ToInt32(db["Hydraulics"]);
            Livery = Convert.ToInt32(db["Livery"]);
            Plate = Convert.ToInt32(db["Plate"]);
            WindowTint = Convert.ToInt32(db["WindowTint"]);
        }

        /** Used internally to update the vehicles last position for when a player logs out. */
        private void updateVehiclePosition()
        {
            Vector3 vehPos = API.getEntityPosition(Handle);
            Vector3 vehRot = API.getEntityRotation(Handle);

            string target = "UPDATE Vehicles SET";
            string where = string.Format("WHERE Id='{0}'", ID);
            string[] variables = { "X", "Y", "Z", "RX", "RY", "RZ" };
            object[] arguments = { vehPos.X, vehPos.Y, vehPos.Z, vehRot.X, vehRot.Y, vehRot.Z };
            Payload.addNewPayload(target, where, variables, arguments);
        }

        public void updateVehicleMods()
        {
            API.setVehicleCustomPrimaryColor(vehicle, PrimaryColor.Item1, PrimaryColor.Item2, PrimaryColor.Item3);
            API.setVehicleCustomSecondaryColor(vehicle, SecondaryColor.Item1, SecondaryColor.Item2, SecondaryColor.Item3);
            API.setVehicleMod(vehicle, 0, Spoiler);
            API.setVehicleMod(vehicle, 1, FrontBumper);
            API.setVehicleMod(vehicle, 2, RearBumper);
            API.setVehicleMod(vehicle, 3, SideSkirt);
            API.setVehicleMod(vehicle, 4, Exhaust);
            API.setVehicleMod(vehicle, 5, Frame);
            API.setVehicleMod(vehicle, 6, Grille);
            API.setVehicleMod(vehicle, 7, Hood);
            API.setVehicleMod(vehicle, 8, Fender);
            API.setVehicleMod(vehicle, 9, RightFender);
            API.setVehicleMod(vehicle, 10, Roof);
            API.setVehicleMod(vehicle, 11, Engine);
            API.setVehicleMod(vehicle, 12, Brakes);
            API.setVehicleMod(vehicle, 13, Transmission);
            API.setVehicleMod(vehicle, 14, Horns);
            API.setVehicleMod(vehicle, 15, Suspension);
            API.setVehicleMod(vehicle, 16, Armor);
            API.setVehicleMod(vehicle, 18, Turbo);
            API.setVehicleMod(vehicle, 22, Xenon);
            API.setVehicleMod(vehicle, 23, FrontWheels);
            API.setVehicleMod(vehicle, 24, BackWheels);
            API.setVehicleMod(vehicle, 25, PlateHolders);
            API.setVehicleMod(vehicle, 27, TrimDesign);
            API.setVehicleMod(vehicle, 28, Ornaments);
            API.setVehicleMod(vehicle, 30, DialDesign);
            API.setVehicleMod(vehicle, 33, SteeringWheel);
            API.setVehicleMod(vehicle, 34, ShiftLever);
            API.setVehicleMod(vehicle, 35, Plaques);
            API.setVehicleMod(vehicle, 38, Hydraulics);
            API.setVehicleMod(vehicle, 48, Livery);
            API.setVehicleMod(vehicle, 62, Plate);
            API.setVehicleMod(vehicle, 69, WindowTint);
        }
    }
}
