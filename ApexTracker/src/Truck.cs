using System.ComponentModel;
using System.Runtime.CompilerServices;
using SCSSdkClient;

namespace ApexTracker;

public class Truck: INotifyPropertyChanged
{
    private string _brand = "";
    public string Brand
    {
        get => _brand;
        set
        {
            if (_brand == value) return;
            _brand = value;
            OnPropertyChanged();
        }
    }

    private SCSGame _game;
    public SCSGame Game
    {
        get => _game;
        set
        {
            if (_game == value) return;
            _game = value;
            OnPropertyChanged();
        }
    }

    private string _brandId = "";
    public string BrandId
    {
        get => _brandId;
        set
        {
            if (_brandId == value) return;
            _brandId = value;
            OnPropertyChanged();
        }
    }

    private string _name = "";
    public string Name
    {
        get => _name;
        set
        {
            if (_name == value) return;
            _name = value;
            OnPropertyChanged();
        }
    }

    private string _id = "";
    public string Id
    {
        get => _id;
        set
        {
            if (_id == value) return;
            _id = value;
            OnPropertyChanged();


        }
    }

    private float _odometer;
    public float Odometer
    {
        get
        {
            if (_game == SCSGame.Ats)
            {
                return (float) (_odometer * 0.621371192);
            }
            return _odometer;
        }
        set
        {
            if (_odometer.Equals(value)) return;
            _odometer = value;
            OnPropertyChanged();
        }
    }

    private float _fuel;
    public float Fuel
    {
        get
        {
            if (_fuelCapacity == 0)
            {
                return 0;
            }
            return (_fuel / _fuelCapacity) * 100;
        }
        set
        {
            if(_fuel.Equals(value)) return;
            _fuel = value;
            OnPropertyChanged();
        }
    }

    private float _fuelCapacity ;
    public float FuelCapacity
    {
        get => _fuelCapacity;
        set
        {
            if(_fuelCapacity.Equals(value)) return;
            _fuelCapacity = value;
            OnPropertyChanged();
        }
    }

    public void ResetAlLValues()
    {
        Brand = "";
        BrandId = "";
        Id = "";
        Name = "";
        FuelCapacity = 0;
        Fuel = 0;
        Odometer = 0;
        Game = SCSGame.Unknown;
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}