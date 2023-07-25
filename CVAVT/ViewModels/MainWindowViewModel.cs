﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CVAVT.Models;
using CVAVT.ViewModels;
using CVAVT.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CVAVT.ViewModels
{
    class MainWindowViewModel : WpfLibrary.BaseViewModel
    {
        // Neuer Teilnehmer
        public ICommand NeuTeilnehmerCmd { get; set; }

        // Neuer Leiter
        public ICommand NeuLeiterCmd { get; set; }

        // Neue Aktivität
        public ICommand NeuAktivitaetCmd { get; set; }

        // Edit Aktivität
        public ICommand EditAktiviaetCmd { get; set; }

        // Teilnehmer Liste Zeigen
        public ICommand ShowTeilnehmerCmd { get; set; }

        // Program Beenden
        public ICommand BeendenCmd { get; set; }

        // Properties
        public ObservableCollection<Aktivitaet> AktivitaetenListe { get; set; }
        public Aktivitaet SelectedAktivitaet { get; set; }

        private int _position;
        private const int Anzahl = 10;
        // Aktivitaet  Properties
        public string AktivitaetenName { get; set; }
        public string AktivitaetenLeiter { get; set; }
        public string AktivitaetenArt { get; set; }
        public DateOnly AktivitaetenDatum { get; set; }
        public TimeOnly AktivitaetenZeit { get; set; }
        public double AktivitaetenDauer { get; set; }
        public int AktivitaetenMaxTeilnehmer { get; set; }
        public bool AktivitaetenVorwissenNoetig { get; set; }
        public string AktivitaetenInformation { get; set; }

        // Konstruktor
        public MainWindowViewModel()
        {
            AktivitaetenListe = new ObservableCollection<Aktivitaet>();
            NeuTeilnehmerCmd = new WpfLibrary.RelayCommand(NeuerTeilnehmerMenu);
            NeuLeiterCmd = new WpfLibrary.RelayCommand(NeuLeiter);
            NeuAktivitaetCmd = new WpfLibrary.RelayCommand(NeuAktivitaet);
            EditAktiviaetCmd = new WpfLibrary.RelayCommand(EditAktivitaet);
            ShowTeilnehmerCmd = new WpfLibrary.RelayCommand(ShowTeilnehmer);
            BeendenCmd = new WpfLibrary.RelayCommand(Schliessen);
            FillList();
        }

        private void Schliessen()
        {
            throw new NotImplementedException();
        }

        private void ShowTeilnehmer()
        {
            TeilnehmerListe window = new TeilnehmerListe();
            window.Show();
        }

        private void EditAktivitaet()
        {
            EditActivityWindow window = new EditActivityWindow();
            window.ShowDialog();
        }

        private void NeuerTeilnehmerMenu()
        {
            NeuerTeilnehmer window = new NeuerTeilnehmer();
            window.ShowDialog();

        }

        private void NeuLeiter()
        {
            NeuerLeiter window = new NeuerLeiter();
            window.ShowDialog();
        }

        private void NeuAktivitaet()
        {
            NeueAktivitaet window = new NeueAktivitaet();
            window.ShowDialog();

        }

        // ------------------------------------------------------

        private void FillList()
        {
            AktivitaetenListe.Clear();
            // DB zugriff
            using (CVAVTContext context = new CVAVTContext())
            {
                var aktivitaeten = context.Aktivitaet.Include(a => a.LeiterIdfkNavigation)
                    .Where(p => AktivitaetenName.IsNullOrEmpty() ? true : p.AktivitaetenName.StartsWith(AktivitaetenName))
                    .Where(p => AktivitaetenArt.IsNullOrEmpty() ? true : p.AktivitaetenArt.StartsWith(AktivitaetenArt))

                    .Skip(_position).Take(Anzahl);


                foreach (Aktivitaet aktivity in aktivitaeten)
                {
                    AktivitaetenListe.Add(aktivity);
                }
            }

        }

    }
}
