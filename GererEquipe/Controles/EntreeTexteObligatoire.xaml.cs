namespace GererEquipe.Controles;

public partial class EntreeTexteObligatoire : ContentView
{
    public static readonly BindableProperty LongueurMaxChampEditionProperty
        = BindableProperty.Create(nameof(LongueurMaxChampEdition),
                                  typeof(int),
                                  typeof(EntreeTexteObligatoire),
                                  0,
                                  BindingMode.TwoWay,
                                  null,
                                  LongueurMaxChampEditionChanged);

    public int LongueurMaxChampEdition
    {
        get => (int)GetValue(LongueurMaxChampEditionProperty);
        set => SetValue(LongueurMaxChampEditionProperty, value);
    }

    private static void LongueurMaxChampEditionChanged(BindableObject pBindable, object pVieilleValeur, object pNouvelleValeur)
    {
        var monControle = (EntreeTexteObligatoire)pBindable;
        monControle.txtTexteAValider.MaxLength = (int)pNouvelleValeur;
    }

    public static readonly BindableProperty FormatExempleEntryProperty =
        BindableProperty.Create(nameof(FormatExempleEntry),
                                typeof(string),
                                typeof(EntreeTexteObligatoire),
                                string.Empty,
                                BindingMode.TwoWay,
                                null,
                                OnFormatExempleEntryChanged);

    public string FormatExempleEntry
    {
        get => (string)GetValue(FormatExempleEntryProperty);
        set => SetValue(FormatExempleEntryProperty, value);
    }

    private static void OnFormatExempleEntryChanged(BindableObject pBindable, object pVieilleValeur, object pNouvelleValeur)
    {
        var monControle = (EntreeTexteObligatoire)pBindable;
        monControle.txtTexteAValider.Placeholder = (string)pNouvelleValeur;
    }

    public static readonly BindableProperty ValeurProperty =
        BindableProperty.Create(nameof(Valeur),
                                typeof(string),
                                typeof(EntreeTexteObligatoire),
                                string.Empty,
                                BindingMode.OneWay,
                                null,
                                OnValeurChanged);

    public string Valeur
    {
        get => (string)GetValue(ValeurProperty);
        set
        {
            try
            {
                SetValue(ValeurProperty, value);
            }
            catch
            {
                // Fait rien là à part laisser afficher l'erreur
                // Bin oui le return false du EstValeurValide throw une exception.
            }
        }
    }

    private static void OnValeurChanged(BindableObject pBindable, object pVieilleValeur, object pNouvelleValeur)
    {
        var monControle = (EntreeTexteObligatoire)pBindable;
        monControle.txtTexteAValider.Text = pNouvelleValeur == null ? string.Empty : pNouvelleValeur.ToString();
    }

    public static readonly BindableProperty EstValeurValideProperty =
        BindableProperty.Create(nameof(EstValeurValide),
                                typeof(bool),
                                typeof(EntreeTexteObligatoire),
                                false,
                                BindingMode.TwoWay);

    public bool EstValeurValide
    {
        get => (bool)GetValue(EstValeurValideProperty);
        set => SetValue(EstValeurValideProperty, value);
    }

    public EntreeTexteObligatoire()
	{
		InitializeComponent();
        ChangerEtatValidite(!string.IsNullOrEmpty(txtTexteAValider.Text));
    }

    void ChangerEtatValidite(bool estValide)
    {
        string etatVisuel = estValide ? "Valide" : "Invalide";
        VisualStateManager.GoToState(layoutParent, etatVisuel);
    }

    private void txtTexteAValider_TextChanged(object sender, TextChangedEventArgs e)
    {
        EstValeurValide = !string.IsNullOrEmpty(txtTexteAValider.Text);
        ChangerEtatValidite(EstValeurValide);
    }
}