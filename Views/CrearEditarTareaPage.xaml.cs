using MiAgendaUTN.ViewModels;

namespace MiAgendaUTN.Views;

public partial class CrearEditarTareaPage : ContentPage
{
    public CrearEditarTareaPage(CrearEditarTareaViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}