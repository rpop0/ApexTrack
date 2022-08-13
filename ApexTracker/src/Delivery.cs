using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ApexTracker;

public class Delivery: INotifyPropertyChanged
{

    private bool _started;
    public bool Started
    {
        get => _started;
        set
        {
            if(_started == value) return;
            _started = value;
            OnPropertyChanged();
        }
    }
    
    private string _cityDestination = "";
    public string CityDestination
    {
        get => _cityDestination;
        set
        {
            if (_cityDestination == value) return;
            _cityDestination = value;
            OnPropertyChanged();
        }
    }
    
    private string _citySource = "";
    public string CitySource
    {
        get => _citySource;
        set
        {
            if (_citySource == value) return;
            _citySource = value;
            OnPropertyChanged();
        }
    }
    
    private string _companyDestination = "";
    public string CompanyDestination
    {
        get => _companyDestination;
        set
        {
            if (_companyDestination == value) return;
            _companyDestination = value;
            OnPropertyChanged();
        }
    }
    
    private string _companySource = "";
    public string CompanySource
    {
        get => _companySource;
        set
        {
            if (_companySource == value) return;
            _companySource = value;
            OnPropertyChanged();
        }
    }
    
    private string _cargoName = "";
    public string CargoName
    {
        get => _cargoName;
        set
        {
            if (_cargoName == value) return;
            _cargoName = value;
            OnPropertyChanged();
        }
    }
    // KG mass
    private float _cargoMass;
    public float CargoMass
    {
        get => _cargoMass;
        set
        {
            if (_cargoMass.Equals(value)) return;
            _cargoMass = value;
            OnPropertyChanged();
        }
    }
    
    private float _cargoDamage;
    public float CargoDamage
    {
        get => _cargoDamage;
        set
        {
            if (_cargoDamage.Equals(value)) return;
            _cargoDamage = value;
            OnPropertyChanged();
        }
    }
    
    private float _odometerStart;
    public float OdometerStart
    {
        get => _odometerStart;
        set
        {
            if (_odometerStart.Equals(value)) return;
            _odometerStart = value;
            OnPropertyChanged();
        }
    }
    
    // private float _odometerEnd;
    // public float OdometerEnd
    // {
    //     get => _odometerEnd;
    //     set
    //     {
    //         if (_odometerEnd.Equals(value)) return;
    //         _odometerEnd = value;
    //         OnPropertyChanged();
    //     }
    // }
    
    private ulong _income;
    public ulong Income
    {
        get => _income;
        set
        {
            if (_income.Equals(value)) return;
            _income = value;
            OnPropertyChanged();
        }
    }

    public void ResetAlLValues()
    {
        Started = false;
        CityDestination = "";
        CitySource = "";
        CompanyDestination = "";
        CompanySource = "";
        CargoName = "";
        CargoDamage = 0;
        CargoMass = 0;
        OdometerStart = 0;
        Income = 0;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}