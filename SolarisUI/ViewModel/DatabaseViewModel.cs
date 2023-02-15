namespace SolarisUI;

using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Microsoft.Win32;
using Newtonsoft.Json;
using RealData;

public class DatabaseViewModel : BaseNotifyPropertyChanged
{
    private WindowViewModel _ownerWindowViewModel;

    public ICommand LoadFromFileCommand { get; set; }
    public ICommand SaveCommand { get; set; }

    public DatabaseViewModel(WindowViewModel ownerWindowViewModel, string pathToLoad)
    {
        _ownerWindowViewModel = ownerWindowViewModel;

        _ownerWindowViewModel.CelestialViewModels = new ObservableCollection<CelestialViewModel>();
        _ownerWindowViewModel.CelestialViewModels.Add(new CelestialViewModel(new CelestialModel("Sun",
                (decimal)1.9891e9, 696340,
                new Coordinates(0.0, 0.0, 0.0, 0.0, 0.0, 0.0, PositionUnits.AU, SpeedUnits.AU_DAYS),
                new OrbitModel(null)),
            0.0, 0.0, 0.0, 1.0));

        LoadDatabase(Path.GetFullPath(Directory.GetCurrentDirectory() + pathToLoad));

        LoadFromFileCommand = new BaseCommand(LoadFromFileCommandAction);
        SaveCommand = new BaseCommand(SaveCommandAction);
    }

    private void LoadFromFileCommandAction()
    {
        var loadFileDialog = new OpenFileDialog();
        loadFileDialog.Filter = "Horizon Data File|*.txt";
        loadFileDialog.Title = "Load Horizons Data File";
        loadFileDialog.InitialDirectory = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\..\Data");
        var loadStatus = (bool)loadFileDialog.ShowDialog();

        if (loadStatus && loadFileDialog.FileName != string.Empty) LoadFromFile(loadFileDialog.FileName);
    }

    private void LoadFromFile(string fullPath)
    {
        using var streamReader = new StreamReader(fullPath);
        var realDataModel = RealDataReader.ReadRealCelestialDataFromStreamReader(streamReader);
        var viewParametersSource = _ownerWindowViewModel.CelestialViewModels.First().OrbitViewModel;
        _ownerWindowViewModel.CelestialViewModels.Add(new CelestialViewModel(realDataModel,
            viewParametersSource.ViewOrbitCenterX, viewParametersSource.ViewOrbitCenterY,
            viewParametersSource.ViewOrbitAngle, viewParametersSource.Zoom));
    }

    private void LoadDatabase(string fullPath)
    {
        foreach (var file in Directory.GetFiles(fullPath)) LoadFromFile(file);
    }

    private void SaveCommandAction()
    {
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "Json file|*.json";
        saveFileDialog.Title = "Save Database";
        saveFileDialog.InitialDirectory = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\..\..\Saves");
        var saveStatus = (bool)saveFileDialog.ShowDialog();

        if (saveStatus && saveFileDialog.FileName != string.Empty)
        {
            var serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            using (var streamWriter = new StreamWriter(saveFileDialog.FileName))
            using (var jsonWriter = new JsonTextWriter(streamWriter))
            {
                //                   serializer.Serialize(jsonWriter, _ownerWindowViewModel.CelestialViewModels.Select(cvm => cvm.CelestialModel).ToList());
            }
        }
    }
}