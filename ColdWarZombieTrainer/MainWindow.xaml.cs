﻿using System;
using System.ComponentModel;
using System.Numerics;
using System.Threading;
using System.Windows;

namespace ColdWarZombieTrainer
{
    public partial class MainWindow : Window
    {
        private WpfConsole _console;
        private bool _started = false;
        private Core _core;

        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();

        private bool _infiniteAmmo;
        private bool _infiniteMoney;
        private bool _instantKill;
        private bool _teleportZombies;
        private bool _rapidFire;
        private bool _teleportZombiesLocation;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_started)
            {
                _started = !_started;
                _core = new Core(_console);
                _core.Start();
            }
        }

        private void GodModeEnable(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("God Mode Enabled");
                _core.godMode.EnableGodMode();
            }
        }

        private void GodModeDisable(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("God Mode Disabled");
                _core.godMode.DisableGodMode();
            }
        }

        private void SpeedHackEnabled(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Speed Hack Enabled");
                _core.speedMultiplier.SetSpeed((float)SpeedHackValueSlider.Value);
            }
        }

        private void SpeedHackDisable(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Speed Hack Disabled");
                _core.speedMultiplier.SetSpeed(1f);
            }
        }

        private void InfiniteAmmoEnable(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Infinite Ammo Enabled");
                _infiniteAmmo = true;
            }

        }

        private void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(100);

                try
                {
                    if (!_started)
                        continue;

                    if (_infiniteAmmo)
                        _core.infiniteAmmo.DoInfiniteAmmo();

                    if (_infiniteMoney)
                        _core.moneyHack.InfiniteMoney();

                    if (_instantKill)
                        _core.zombieHack.OneHpZombies();

                    if (_teleportZombies)
                        _core.zombieHack.TeleportZombies(true, 150);

                    if (_rapidFire)
                        _core.miscFeatures.DoRapidFire();

                    if (_teleportZombiesLocation)
                        _core.zombieHack.TeleportZombies(false);
                }
                catch (Exception exception)
                {
                    _console.WriteLine(exception.Message);
                }

            }
        }

        private void InfiniteAmmoDisable(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Infinite Ammo Disabled");
                _infiniteAmmo = false;
            }

        }

        private void InfiniteMoneyHack(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Infinite Money Enabled");
                _infiniteMoney = true;
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _console = new WpfConsole(Console);
            _backgroundWorker.DoWork += BackgroundWorkerDoWork;
            _backgroundWorker.RunWorkerAsync();
        }

        private void InfiniteMoneyDisable(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Infinite Money Disabled");
                _infiniteMoney = false;
            }
        }

        private void InstantKillEnable(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Instant Kill Enabled");
                _instantKill = true;
            }
        }

        private void InstantKillDisable(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Instant Kill Disabled");
                _instantKill = false;
            }
        }

        private void TeleportZombiesEnabled(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                if (TeleportZombiePositionCheckBox.IsChecked.GetValueOrDefault())
                    TeleportZombiePositionCheckBox.IsChecked = false;

                _console.WriteLine("Teleport Zombies Too Crosshair Enabled");
                _teleportZombies = true;
            }
        }

        private void TeleportZombiesDisable(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Teleport Zombies Disabled");
                _teleportZombies = false;
            }
        }

        private void XpModiferEnabled(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("XP Multiplier enabled");
                _core.xpMultiplier.PlayerXpMultiplier((float) XpModiferSlider.Value);
            }
        }

        private void XpModiferDisabled(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("XP Multiplier disabled");
                _core.xpMultiplier.PlayerXpMultiplier(1f);
            }
        }

        private void GunXpModiferEnabled(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("XP Multiplier Started");
                _core.xpMultiplier.GunXpMultiplier((float)XpModiferSlider.Value);
            }
        }

        private void GunXpModiferDisabled(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("XP Multiplier disabled");
                _core.xpMultiplier.GunXpMultiplier(1f);
            }
        }

        private void TimeScaleEnabled(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Time Scaled Speed Enabled");
                _core.speedMultiplier.SetTimeScale((float)TimeScaleModiferSlider.Value);
            }
        }

        private void TimeScaleDisabled(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Time Scaled Speed Disabled");
                _core.speedMultiplier.SetTimeScale(1f);
            }
        }

        private void InfraredEnable(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Infrared enabled");
            _core.miscFeatures.ToggleInfraredVision();
            }

        }

        private void InfraredDisable(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Infrared enabled");
                _core.miscFeatures.ToggleInfraredVision();
            }
        }

        private void RapidFireEnabled(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Rapid Fire enabled");
                _rapidFire = true;
            }
        }

        private void RapidFireDisable(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Rapid Fire Disabled");
                _rapidFire = false;
            }
        }

        private void HeadShotOnlyEnabled(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Head Shot Only Enabled");
                _core.miscFeatures.ToggleCritOnly();
            }
        }

        private void HeadShotOnlyDisable(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Head Shot Only Disabled");
                _core.miscFeatures.ToggleCritOnly();
            }
        }

        private void SetPosition(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                Vector3 position = _core.zombieHack.SetPosition();
                PositionLabel.Content = $"Set Position: [{position.X},{position.Y},{position.Z}]";
            }
        }

        private void TeleportZombiesPosEnabled(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Teleporting Zombies To Location Enabled");

                if (TeleportZombieCheckBox.IsChecked.GetValueOrDefault())
                    TeleportZombieCheckBox.IsChecked = false;

                _teleportZombiesLocation = true;
            }
        }

        private void TeleportZombiesPosDisabled(object sender, RoutedEventArgs e)
        {
            if (_started)
            {
                _console.WriteLine("Teleporting Zombies To Location Disable");
                _teleportZombiesLocation = false;

            }


        }
    }
}
