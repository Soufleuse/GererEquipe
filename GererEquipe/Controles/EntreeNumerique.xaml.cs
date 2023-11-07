namespace GererEquipe.Controles;

public partial class EntreeNumerique : ContentView
{
    public static readonly BindableProperty EstLectureSeuleProperty =
        BindableProperty.Create(nameof(EstLectureSeule),
                                typeof(bool),
                                typeof(EntreeNumerique),
                                false,
                                BindingMode.OneWay,
                                null,
                                OnEstLectureSeuleChanged);

    public bool EstLectureSeule
    {
        get => (bool)GetValue(EstLectureSeuleProperty);
        set => SetValue(EstLectureSeuleProperty, value);
    }

    private static void OnEstLectureSeuleChanged(BindableObject pBindable, object pVieilleValeur, object pNouvelleValeur)
    {
        var monControle = (EntreeNumerique)pBindable;
        monControle.txtValeur.IsReadOnly = (bool)pNouvelleValeur;
    }

    public static readonly BindableProperty ValeurParDefautProperty =
		BindableProperty.Create(nameof(ValeurParDefaut), typeof(Int32?), typeof(EntreeNumerique), 0);

    public Int32? ValeurParDefaut
	{
		get => (Int32?)GetValue(ValeurParDefautProperty);
		set
		{
			try
			{
				SetValue(ValeurParDefautProperty, value);
				SetValue(ValeurProperty, value);
			}
			catch
			{
                SetValue(ValeurParDefautProperty, ValeurMinimum);
                SetValue(ValeurProperty, ValeurMinimum);
            }
		}
	}

	public static readonly BindableProperty ValeurMinimumProperty =
		BindableProperty.Create(nameof(ValeurMinimum), typeof(Int32), typeof(EntreeNumerique), Int32.MinValue);

	public Int32 ValeurMinimum
	{
		get => (Int32)GetValue(ValeurMinimumProperty);
		set => SetValue(ValeurMinimumProperty, value);
    }

    public static readonly BindableProperty ValeurMaximumProperty =
        BindableProperty.Create(nameof(ValeurMaximum), typeof(Int32), typeof(EntreeNumerique), Int32.MaxValue);

    public Int32 ValeurMaximum
    {
        get => (Int32)GetValue(ValeurMaximumProperty);
        set => SetValue(ValeurMaximumProperty, value);
    }

    public static readonly BindableProperty PermettreValeurNullProperty =
        BindableProperty.Create(nameof(PermettreValeurNull), typeof(bool), typeof(EntreeNumerique), true);

    public bool PermettreValeurNull
	{
		get => (bool)GetValue(PermettreValeurNullProperty);
		set => SetValue(PermettreValeurNullProperty, value);
	}

    public static readonly BindableProperty FormatExempleEntryProperty =
		BindableProperty.Create(nameof(FormatExempleEntry),
                                typeof(string),
                                typeof(EntreeNumerique),
                                string.Empty,
                                BindingMode.OneWay,
                                null,
                                OnFormatExempleEntryChanged);

	public string FormatExempleEntry
	{
		get => (string)GetValue(FormatExempleEntryProperty);
		set => SetValue(FormatExempleEntryProperty, value);
	}

    private static void OnFormatExempleEntryChanged(BindableObject pBindable, object pVieilleValeur, object pNouvelleValeur)
    {
        var monControle = (EntreeNumerique)pBindable;
        monControle.txtValeur.Placeholder = (string)pNouvelleValeur;
    }

    public static readonly BindableProperty LibelleErreurProperty =
		BindableProperty.Create(nameof(LibelleErreur),
								typeof(string),
								typeof(EntreeNumerique),
								string.Empty,
								BindingMode.OneWay,
								null,
								OnLibelleErreurChanged);

	public string LibelleErreur
	{
		get => (string)GetValue(LibelleErreurProperty);
		set => SetValue(LibelleErreurProperty, value);
    }

    private static void OnLibelleErreurChanged(BindableObject pBindable, object pVieilleValeur, object pNouvelleValeur)
    {
        var monControle = (EntreeNumerique)pBindable;
        monControle.lblMessageErreur.Text = (string)pNouvelleValeur;
    }

    public static readonly BindableProperty ValeurProperty = 
		BindableProperty.Create(nameof(Valeur),
								typeof(Int32?),
								typeof(EntreeNumerique),
								0,
                                BindingMode.TwoWay,
                                IsValeurValide,
                                OnValeurChanged);

	public Int32? Valeur
	{
		get => (Int32?)GetValue(ValeurProperty);
		set
		{
			try
			{
				SetValue(ValeurProperty, value);
			}
			catch
			{
				// Fait rien là à part laisser afficher l'erreur
				// Bin oui le return false du IsValeurValide throw une exception.
			}
		}
    }

    private static bool IsValeurValide(BindableObject pView, object pValue)
    {
        var monEntree = (EntreeNumerique)pView;
        monEntree.LibelleErreur = string.Empty;

        if (pValue == null)
        {
            if (!monEntree.PermettreValeurNull)
            {
                monEntree.LibelleErreur = "La valeur ne peut pas être nulle.";
                return false;
            }
        }
        else
        {
            if (!Int32.TryParse(pValue.ToString(), out Int32 resultat))
            {
                monEntree.LibelleErreur = "La valeur est invalide.";
                return false;
            }

            if (resultat < monEntree.ValeurMinimum || resultat > monEntree.ValeurMaximum)
            {
                monEntree.LibelleErreur = "La valeur est trop petit ou trop grande.";
                return false;
            }
        }

        return true;
    }

    private static void OnValeurChanged(BindableObject pBindable, object pVieilleValeur, object pNouvelleValeur)
    {
        var monControle = (EntreeNumerique)pBindable;
        monControle.txtValeur.Text = pNouvelleValeur.ToString();
    }

    public EntreeNumerique()
	{
		InitializeComponent();
    }

    private void EntreeNumerique_Loaded(object sender, EventArgs e)
    {
		if (ValeurParDefaut.HasValue)
		{
			if (!Valeur.HasValue)
			{
				Valeur = ValeurParDefaut;
				txtValeur.Text = ValeurParDefaut.Value.ToString();
			}
			else
            {
                txtValeur.Text = Valeur.Value.ToString();
            }
		}
        else if (Valeur.HasValue)
        {
            txtValeur.Text = Valeur.Value.ToString();
        }
    }

    private void txtValeur_Unfocused(object sender, FocusEventArgs e)
    {
		var entree = (Entry)sender;
		if (entree == null) { return; }

		var monEntreeNumerique = (EntreeNumerique)entree.Parent.Parent;
		LibelleErreur = string.Empty;
        Valeur = monEntreeNumerique.ValeurMinimum;

		if (string.IsNullOrEmpty(entree.Text))
		{
			if (PermettreValeurNull)
            {
                Valeur = null;
                entree.Text = string.Empty;
            }
            else
            {
				// La valeur est déjà à la valeur minimum.
				entree.Text = Valeur.ToString();
            }

            return;
		}

        if (!Int32.TryParse(entree.Text, out Int32 val))
        {
            LibelleErreur = "Le champ doit être numérique.";
            return;
        }

        Valeur = val;
    }
}