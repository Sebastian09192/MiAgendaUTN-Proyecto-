using MiAgendaUTN.ViewModels;

namespace MiAgendaUTN.Views;

public partial class ListaTareasPage : ContentPage
{
    public ListaTareasPage(ListaTareasViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ListaTareasViewModel vm)
            vm.OnAppearing();
    }
}