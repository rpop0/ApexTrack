using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using SCSSdkClient;
using SCSSdkClient.Object;

namespace ApexTracker;

public sealed class TrackerService: INotifyPropertyChanged
{
    
    private Truck _currentTruck;

    private readonly SCSSdkTelemetry _telemetry;
    public Truck CurrentTruck
    {
        get => _currentTruck;
        private set
        {
            _currentTruck = value;
            OnPropertyChanged();
        }
    }
    
    private bool _simulatorRunning;
    public bool SimulatorRunning
    {
        get => _simulatorRunning;
        set
        {
            _simulatorRunning = value;
            OnPropertyChanged();
        }
    }

    public TrackerService()
    {
        _currentTruck = new Truck();
        _telemetry = new SCSSdkTelemetry();
        _telemetry.Data += Telemetry_Data;
    }
    
    
    private void Telemetry_Data(SCSTelemetry data, bool updated)
    {
        if (!updated) {
            return;
        }

        try {
            if (data.Game == SCSGame.Unknown)
            {
                CurrentTruck.Brand = "None";
                CurrentTruck.BrandId = "None";
                CurrentTruck.Id = "None";
                CurrentTruck.Name = "None";
                CurrentTruck.FuelCapacity = 0;
                CurrentTruck.Fuel = 0;
                CurrentTruck.Odometer = 0;
                SimulatorRunning = false;
                return;
            }
            SimulatorRunning = true;
            
            if (data.TruckValues.ConstantsValues.Id != CurrentTruck.Id && (int)data.TruckValues.ConstantsValues.CapacityValues.Fuel != (int)CurrentTruck.FuelCapacity)
            {
                CurrentTruck.Brand = data.TruckValues.ConstantsValues.Brand;
                CurrentTruck.BrandId = data.TruckValues.ConstantsValues.BrandId;
                CurrentTruck.Id = data.TruckValues.ConstantsValues.Id;
                CurrentTruck.Name = data.TruckValues.ConstantsValues.Name;
                CurrentTruck.FuelCapacity = data.TruckValues.ConstantsValues.CapacityValues.Fuel;
            }
            CurrentTruck.Odometer = data.TruckValues.CurrentValues.DashboardValues.Odometer;
            CurrentTruck.Fuel = data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount;


            if (!data.SpecialEventsValues.OnJob) {
                return;
            }
            
            
        }
        catch (Exception ex)
        {
            // ignored
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}