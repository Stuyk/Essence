using Essence.classes.utility;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
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

        public VehicleHash Type { get; set; }
        public NetHandle VehicleHandle { get; set; }
        public Tuple<int, int, int> primaryColor;
        public Tuple<int, int, int> secondaryColor;
        public Vector3 LastPosition { get; set; }
        public Vector3 LastRotation { get; set; }
        public int Id { get; set; }
        public int ColorA { get; set; }
        public int ColorB { get; set; }
        public int Spoiler { get; set; }
        public int FrontBumper { get; set; }
        public int RearBumper { get; set; }
        public int SideSkirt { get; set; }
        public int Exhaust { get; set; }
        public int Frame { get; set; }
        public int Grille { get; set; }
        public int Hood { get; set; }
        public int Fender { get; set; }
        public int RightFender { get; set; }
        public int Roof { get; set; }
        public int Engine { get; set; }
        public int Brakes { get; set; }
        public int Transmission { get; set; }
        public int Horns { get; set; }
        public int Suspension { get; set; }
        public int Armor { get; set; }
        public int Turbo { get; set; }
        public int Xenon { get; set; }
        public int FrontWheels { get; set; }
        public int BackWheels { get; set; }
        public int PlateHolders { get; set; }
        public int TrimDesign { get; set; }
        public int Ornaments { get; set; }
        public int DialDesign { get; set; }
        public int SteeringWheel { get; set; }
        public int ShiftLever { get; set; }
        public int Plaques { get; set; }
        public int Hydraulics { get; set; }
        public int Livery { get; set; }
        public int Plate { get; set; }
        public int WindowTint { get; set; }

        public Tuple<int, int, int> PrimaryColor { get; set; }
        public Tuple<int, int, int> SecondaryColor { get; set; }

        public Vehicle() { }

        public Vehicle(DataRow db)
        {
            Id = Convert.ToInt32(db["Id"]);
            Type = API.vehicleNameToModel(Convert.ToString(db["Type"]));
            LastPosition = new Vector3(Convert.ToSingle(db["X"]), Convert.ToSingle(db["Y"]), Convert.ToSingle(db["Z"]));
            LastRotation = new Vector3(Convert.ToSingle(db["RX"]), Convert.ToSingle(db["RY"]), Convert.ToSingle(db["RZ"]));
            ColorA = Convert.ToInt32(db["ColorA"]);
            ColorA = Convert.ToInt32(db["ColorB"]);
            VehicleHandle = API.createVehicle(Type, LastPosition, LastRotation, ColorA, ColorB);
            API.setVehicleEngineStatus(VehicleHandle, false);
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
            Vector3 vehPos = API.getEntityPosition(VehicleHandle);
            Vector3 vehRot = API.getEntityRotation(VehicleHandle);

            string target = "UPDATE Vehicles SET";
            string where = string.Format("WHERE Id='{0}'", Id);
            string[] variables = { "X", "Y", "Z", "RX", "RY", "RZ" };
            object[] arguments = { vehPos.X, vehPos.Y, vehPos.Z, vehRot.X, vehRot.Y, vehRot.Z };
            Payload.addNewPayload(target, where, variables, arguments);
        }

        public void updateVehicleMods()
        {
            API.setVehicleCustomPrimaryColor(VehicleHandle, PrimaryColor.Item1, PrimaryColor.Item2, PrimaryColor.Item3);
            API.setVehicleCustomSecondaryColor(VehicleHandle, SecondaryColor.Item1, SecondaryColor.Item2, SecondaryColor.Item3);
            API.setVehicleMod(VehicleHandle, 0, Spoiler);
            API.setVehicleMod(VehicleHandle, 1, FrontBumper);
            API.setVehicleMod(VehicleHandle, 2, RearBumper);
            API.setVehicleMod(VehicleHandle, 3, SideSkirt);
            API.setVehicleMod(VehicleHandle, 4, Exhaust);
            API.setVehicleMod(VehicleHandle, 5, Frame);
            API.setVehicleMod(VehicleHandle, 6, Grille);
            API.setVehicleMod(VehicleHandle, 7, Hood);
            API.setVehicleMod(VehicleHandle, 8, Fender);
            API.setVehicleMod(VehicleHandle, 9, RightFender);
            API.setVehicleMod(VehicleHandle, 10, Roof);
            API.setVehicleMod(VehicleHandle, 11, Engine);
            API.setVehicleMod(VehicleHandle, 12, Brakes);
            API.setVehicleMod(VehicleHandle, 13, Transmission);
            API.setVehicleMod(VehicleHandle, 14, Horns);
            API.setVehicleMod(VehicleHandle, 15, Suspension);
            API.setVehicleMod(VehicleHandle, 16, Armor);
            API.setVehicleMod(VehicleHandle, 18, Turbo);
            API.setVehicleMod(VehicleHandle, 22, Xenon);
            API.setVehicleMod(VehicleHandle, 23, FrontWheels);
            API.setVehicleMod(VehicleHandle, 24, BackWheels);
            API.setVehicleMod(VehicleHandle, 25, PlateHolders);
            API.setVehicleMod(VehicleHandle, 27, TrimDesign);
            API.setVehicleMod(VehicleHandle, 28, Ornaments);
            API.setVehicleMod(VehicleHandle, 30, DialDesign);
            API.setVehicleMod(VehicleHandle, 33, SteeringWheel);
            API.setVehicleMod(VehicleHandle, 34, ShiftLever);
            API.setVehicleMod(VehicleHandle, 35, Plaques);
            API.setVehicleMod(VehicleHandle, 38, Hydraulics);
            API.setVehicleMod(VehicleHandle, 48, Livery);
            API.setVehicleMod(VehicleHandle, 62, Plate);
            API.setVehicleMod(VehicleHandle, 69, WindowTint);
        }
    }
}
