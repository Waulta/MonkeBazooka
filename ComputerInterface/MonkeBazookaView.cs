using System;
using System.Collections.Generic;
using Utilla;
using ComputerInterface;
using ComputerInterface.ViewLib;
using Photon.Pun;
using UnityEngine;
using MonkeBazooka.Utils;
using MonkeBazooka.Core;

namespace MonkeBazooka.ComputerInterface
{
    class MonkeBazookaView : ComputerView
    {
        public static MonkeBazookaView instance;
        private readonly UISelectionHandler selectionHandler;
        const string highlightColour = "66ff00";

        public MonkeBazookaView()
        {
            instance = this;

            selectionHandler = new UISelectionHandler(EKeyboardKey.Up, EKeyboardKey.Down, EKeyboardKey.Enter);

            selectionHandler.MaxIdx = 2;

            selectionHandler.OnSelected += OnEntrySelected;

            selectionHandler.ConfigureSelectionIndicator($"<color=#{highlightColour}>></color> ", "", "  ", "");
        }

        public override void OnShow(object[] args)
        {
            base.OnShow(args);
            UpdateScreen();
        }

        public void UpdateScreen()
        {
            SetText(str =>
            {
                str.BeginCenter();
                str.MakeBar('-', SCREEN_WIDTH, 0, "ffffff10");
                str.AppendClr("Monke Bazooka", highlightColour).EndColor().AppendLine();
                str.AppendLine("By Waulta");
                str.MakeBar('-', SCREEN_WIDTH, 0, "ffffff10");
                str.EndAlign().AppendLines(1);
                str.AppendLine(selectionHandler.GetIndicatedText(0, $"<color={(MBConfig.Enabled ? string.Format("#{0}>[Enabled]", highlightColour) : "red>[Disabled]")}</color>"));
                str.AppendLines(1);
                str.AppendClr("  Selected Hand:", highlightColour).EndColor().AppendLine();
                str.AppendLine(selectionHandler.GetIndicatedText(1, $"<color={(MBConfig.Left ? "white>[Left]" : "white>[Right]")}</color>"));
                str.AppendLines(1);
                str.AppendClr("  Explosion Force:", highlightColour).EndColor().AppendLine();                
                if (MBConfig.ExplosionForce == 4f)
                {
                    str.AppendLine(selectionHandler.GetIndicatedText(2, "4 (Default)"));
                }
                else
                {
                    str.AppendLine(selectionHandler.GetIndicatedText(2, MBConfig.ExplosionForce.ToString()));
                }
                if (!MBConfig.Modded)
                {
                    str.AppendLines(1);
                    str.AppendClr("           Please join a modded room!", "A01515").EndColor().AppendLine();
                }
            });
        }

        private void OnEntrySelected(int index)
        {
            try
            {
                switch (index)
                {
                    case 0:
                        if (MBConfig.Modded)
                            BazookaManager.Instance.UpdateEnabled();
                        UpdateScreen();
                        break;
                    case 1:
                        if (MBConfig.Modded)
                            BazookaManager.Instance.UpdateLeft();
                        UpdateScreen();
                        break;
                }
            }
            catch (Exception e) { Console.WriteLine(e); }
        }

        public override void OnKeyPressed(EKeyboardKey key)
        {
            if (selectionHandler.HandleKeypress(key))
            {
                UpdateScreen();
                return;
            }
            
            switch(selectionHandler.CurrentSelectionIndex)
            {
                case 1:
                    switch (key)
                    {
                        case EKeyboardKey.Left:
                            if (MBConfig.Modded)
                                BazookaManager.Instance.UpdateLeft();
                            break;
                        case EKeyboardKey.Right:
                            if (MBConfig.Modded)
                                BazookaManager.Instance.UpdateLeft();
                            break;
                    }
                    UpdateScreen();
                    break;
                case 2:
                    switch (key)
                    {
                        case EKeyboardKey.Left:
                            BazookaManager.Instance.UpdateForce(false);
                            break;
                        case EKeyboardKey.Right:
                            BazookaManager.Instance.UpdateForce(true);
                            break;
                    }
                    UpdateScreen();
                    break;
            }

            switch (key)
            {
                case EKeyboardKey.Back:
                    ReturnToMainMenu();
                    break;
                case EKeyboardKey.Up:
                    selectionHandler.MoveSelectionUp();
                    UpdateScreen();
                    break;
                case EKeyboardKey.Down:
                    selectionHandler.MoveSelectionDown();
                    UpdateScreen();
                    break;
            }
        }
    }
}