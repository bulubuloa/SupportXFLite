﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using SupportXFLite.Controllers.Logger;
using SupportXFLite.Models;
using Xamarin.Forms;

namespace SupportXFLite.ViewModels
{
    public abstract partial class XFLiteBaseViewModel : BindableBaseModel, IHandleViewDisappearing, IHandleViewAppearing, ILogger
    {
        /*
         * Validations
         */
        private string _WarningMessage;
        public string WarningMessage
        {
            get => _WarningMessage;
            set
            {
                _WarningMessage = value;
                OnPropertyChanged();
            }
        }

        private bool _IsValidThisForms;
        public bool IsValidThisForms
        {
            get => _IsValidThisForms;
            set
            {
                _IsValidThisForms = value;
                OnPropertyChanged();
            }
        }


        /*
         * View Status
         */
        private bool _IsInitialize;
        public bool IsInitialize
        {
            get => _IsInitialize;
            set
            {
                _IsInitialize = value;
                OnPropertyChanged();
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        private bool _IsRefresh;
        public bool IsRefresh
        {
            get => _IsRefresh;
            set
            {
                _IsRefresh = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshScreenCommand => new AsyncCommand(OnRefreshScreenCommand);
        public virtual Task OnRefreshScreenCommand()
        {
            return Task.FromResult(true);
        }

        public void DebugMessage(string content, string title = "")
        {
            Debug.WriteLine(title + ": " + content);
        }

        protected virtual async void RunActionOnNewThread(Func<Task> action, CancellationTokenSource cancellationToken, bool IsSyncBusy = false, int timeDelay = 100)
        {
            try
            {
                if (IsSyncBusy)
                    IsBusy = true;

                await Task.Delay(timeDelay);
                await Task.Run(action, cancellationToken.Token);
            }
            catch (Exception ex)
            {
                DebugMessage(ex.StackTrace);
            }
            finally
            {
                if (IsSyncBusy)
                    IsBusy = false;
            }
        }

        protected virtual async void RunActionOnNewThread(Action action, bool IsSyncBusy = false, int timeDelay = 100)
        {
            try
            {
                if (IsSyncBusy)
                    IsBusy = true;

                await Task.Delay(timeDelay);
                action();
            }
            catch (Exception ex)
            {
                DebugMessage(ex.StackTrace);
            }
            finally
            {
                if (IsSyncBusy)
                    IsBusy = false;
            }
        }

        protected virtual async void RunActionOnNewThread(Func<Task> action, bool IsSyncBusy = false, int timeDelay = 100)
        {
            try
            {
                if (IsSyncBusy)
                    IsBusy = true;

                await Task.Delay(timeDelay);
                await action();
            }
            catch (Exception ex)
            {
                DebugMessage(ex.StackTrace);
            }
            finally
            {
                if (IsSyncBusy)
                    IsBusy = false;
            }
        }

        public XFLiteBaseViewModel()
        {

        }
    }
}