using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Merlion.ECR.Update.Core.Environment
{
    // C:\Windows\Microsoft.Net\assembly\GAC_MSIL\Microsoft.VisualStudio.Shell.14.0\v4.0_14.0.0.0__b03f5f7f11d50a3a\Microsoft.VisualStudio.Shell.14.0.dll
    // Microsoft.VisualStudio.Shell.14.0, Version=14.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a

    /// <summary>
    /// Копия VSConstants из Microsoft.VisualStudio, вскрытая через ILSpy (лень ещё одну COM-ref ради такого использовать, константы ж)
    /// </summary>
    public sealed class VSConstants
    {
        public static class WellKnownOldVersionValues
        {
            public const string LowestMajor = "LowestMajor";

            public const string LowestMajorMinor = "LowestMajorMinor";

            public const string Current = "Current";
        }

        public static class WellKnownToolboxStringMaps
        {
            public const string MultiTargeting = "MultiTargeting:{FBB22D27-7B21-42ac-88C8-595F94BDBCA5}";
        }

        public static class ToolboxMultitargetingFields
        {
            public const string TypeName = "TypeName";

            public const string AssemblyName = "AssemblyName";

            public const string Frameworks = "Frameworks";

            public const string ItemProvider = "ItemProvider";

            public const string UseProjectTargetFrameworkVersionInTooltip = "UseProjectTargetFrameworkVersionInTooltip";
        }

        public static class CMDSETID
        {
            public const string StandardCommandSet97_string = "{5EFC7975-14BC-11CF-9B2B-00AA00573819}";

            public static readonly Guid StandardCommandSet97_guid = new Guid("{5EFC7975-14BC-11CF-9B2B-00AA00573819}");

            public const string StandardCommandSet2K_string = "{1496A755-94DE-11D0-8C3F-00C04FC2AAE2}";

            public static readonly Guid StandardCommandSet2K_guid = new Guid("{1496A755-94DE-11D0-8C3F-00C04FC2AAE2}");

            public const string StandardCommandSet2010_string = "{5DD0BB59-7076-4C59-88D3-DE36931F63F0}";

            public static readonly Guid StandardCommandSet2010_guid = new Guid("{5DD0BB59-7076-4C59-88D3-DE36931F63F0}");

            public const string StandardCommandSet11_string = "{D63DB1F0-404E-4B21-9648-CA8D99245EC3}";

            public static readonly Guid StandardCommandSet11_guid = new Guid("{D63DB1F0-404E-4B21-9648-CA8D99245EC3}");

            public const string StandardCommandSet12_string = "{2A8866DC-7BDE-4dc8-A360-A60679534384}";

            public static readonly Guid StandardCommandSet12_guid = new Guid("{2A8866DC-7BDE-4dc8-A360-A60679534384}");

            public const string StandardCommandSet14_string = "{4C7763BF-5FAF-4264-A366-B7E1F27BA958}";

            public static readonly Guid StandardCommandSet14_guid = new Guid("{4C7763BF-5FAF-4264-A366-B7E1F27BA958}");

            public const string ShellMainMenu_string = "{D309F791-903F-11D0-9EFC-00A0C911004F}";

            public static readonly Guid ShellMainMenu_guid = new Guid("{D309F791-903F-11D0-9EFC-00A0C911004F}");

            public const string UIHierarchyWindowCommandSet_string = "{60481700-078B-11D1-AAF8-00A0C9055A90}";

            public static readonly Guid UIHierarchyWindowCommandSet_guid = new Guid("{60481700-078B-11D1-AAF8-00A0C9055A90}");

            public const string VsDocOutlinePackageCommandSet_string = "{21AF45B0-FFA5-11D0-B63F-00A0C922E851}";

            public static readonly Guid VsDocOutlinePackageCommandSet_guid = new Guid("{21AF45B0-FFA5-11D0-B63F-00A0C922E851}");

            public const string SolutionExplorerPivotList_string = "{afe48dbb-c199-46ce-ba09-adbd5e933ea3}";

            public static readonly Guid SolutionExplorerPivotList_guid = new Guid("{afe48dbb-c199-46ce-ba09-adbd5e933ea3}");

            public const string CSharpGroup_string = "{5D7E7F65-A63F-46ee-84F1-990B2CAB23F9}";

            public static readonly Guid CSharpGroup_guid = new Guid("{5D7E7F65-A63F-46ee-84F1-990B2CAB23F9}");
        }

        public static class NewDocumentStateReason
        {
            public static readonly Guid FindSymbolResults = VSConstants.StandardToolWindows.FindSymbolResults;

            public static readonly Guid FindResults = VSConstants.StandardToolWindows.Find1;

            public static readonly Guid Navigation = new Guid("8d57e022-9e44-4efd-8e4e-230284f86376");

            public static readonly Guid SolutionExplorer = VSConstants.StandardToolWindows.SolutionExplorer;

            public static readonly Guid TeamExplorer = VSConstants.StandardToolWindows.TeamExplorer;
        }

        [Guid("12F1A339-02B9-46e6-BDAF-1071F76056BF")]
        public enum AppCommandCmdID
        {
            BrowserBackward = 1,
            BrowserForward,
            BrowserRefresh,
            BrowserStop,
            BrowserSearch,
            BrowserFavorites,
            BrowserHome,
            VolumeMute,
            VolumeDown,
            VolumeUp,
            MediaNextTrack,
            MediaPreviousTrack,
            MediaStop,
            MediaPlayPause,
            LaunchMail,
            LaunchMediaSelect,
            LaunchApp1,
            LaunchApp2,
            BassDown,
            BassBoost,
            BassUp,
            TrebleDown,
            TrebleUp,
            MicrophoneVolumeMute,
            MicrophoneVolumeDown,
            MicrophoneVolumeUp
        }

        [Guid("5EFC7975-14BC-11CF-9B2B-00AA00573819")]
        public enum VSStd97CmdID
        {
            AlignBottom = 1,
            AlignHorizontalCenters,
            AlignLeft,
            AlignRight,
            AlignToGrid,
            AlignTop,
            AlignVerticalCenters,
            ArrangeBottom,
            ArrangeRight,
            BringForward,
            BringToFront,
            CenterHorizontally,
            CenterVertically,
            Code,
            Copy,
            Cut,
            Delete,
            FontName,
            FontNameGetList = 500,
            FontSize = 19,
            FontSizeGetList = 501,
            Group = 20,
            HorizSpaceConcatenate,
            HorizSpaceDecrease,
            HorizSpaceIncrease,
            HorizSpaceMakeEqual,
            LockControls = 369,
            InsertObject = 25,
            Paste,
            Print,
            Properties,
            Redo,
            MultiLevelRedo,
            SelectAll,
            SendBackward,
            SendToBack,
            ShowTable,
            SizeToControl,
            SizeToControlHeight,
            SizeToControlWidth,
            SizeToFit,
            SizeToGrid,
            SnapToGrid,
            TabOrder,
            Toolbox,
            Undo,
            MultiLevelUndo,
            Ungroup,
            VertSpaceConcatenate,
            VertSpaceDecrease,
            VertSpaceIncrease,
            VertSpaceMakeEqual,
            ZoomPercent,
            BackColor,
            Bold,
            BorderColor,
            BorderDashDot,
            BorderDashDotDot,
            BorderDashes,
            BorderDots,
            BorderShortDashes,
            BorderSolid,
            BorderSparseDots,
            BorderWidth1,
            BorderWidth2,
            BorderWidth3,
            BorderWidth4,
            BorderWidth5,
            BorderWidth6,
            BorderWidthHairline,
            Flat,
            ForeColor,
            Italic,
            JustifyCenter,
            JustifyGeneral,
            JustifyLeft,
            JustifyRight,
            Raised,
            Sunken,
            Underline,
            Chiseled,
            Etched,
            Shadowed,
            CompDebug1,
            CompDebug2,
            CompDebug3,
            CompDebug4,
            CompDebug5,
            CompDebug6,
            CompDebug7,
            CompDebug8,
            CompDebug9,
            CompDebug10,
            CompDebug11,
            CompDebug12,
            CompDebug13,
            CompDebug14,
            CompDebug15,
            ExistingSchemaEdit,
            Find,
            GetZoom,
            QueryOpenDesign,
            QueryOpenNew,
            SingleTableDesign,
            SingleTableNew,
            ShowGrid,
            NewTable,
            CollapsedView,
            FieldView,
            VerifySQL,
            HideTable,
            PrimaryKey,
            Save,
            SaveAs,
            SortAscending,
            SortDescending,
            AppendQuery,
            CrosstabQuery,
            DeleteQuery,
            MakeTableQuery,
            SelectQuery,
            UpdateQuery,
            Parameters,
            Totals,
            ViewCollapsed,
            ViewFieldList,
            ViewKeys,
            ViewGrid,
            InnerJoin,
            RightOuterJoin,
            LeftOuterJoin,
            FullOuterJoin,
            UnionJoin,
            ShowSQLPane,
            ShowGraphicalPane,
            ShowDataPane,
            ShowQBEPane,
            SelectAllFields,
            OLEObjectMenuButton,
            ObjectVerbList0,
            ObjectVerbList1,
            ObjectVerbList2,
            ObjectVerbList3,
            ObjectVerbList4,
            ObjectVerbList5,
            ObjectVerbList6,
            ObjectVerbList7,
            ObjectVerbList8,
            ObjectVerbList9,
            ConvertObject,
            CustomControl,
            CustomizeItem,
            Rename,
            Import,
            NewPage,
            Move,
            Cancel,
            Font,
            ExpandLinks,
            ExpandImages,
            ExpandPages,
            RefocusDiagram,
            TransitiveClosure,
            CenterDiagram,
            ZoomIn,
            ZoomOut,
            RemoveFilter,
            HidePane,
            DeleteTable,
            DeleteRelationship,
            Remove,
            JoinLeftAll,
            JoinRightAll,
            AddToOutput,
            OtherQuery,
            GenerateChangeScript,
            SaveSelection,
            AutojoinCurrent,
            AutojoinAlways,
            EditPage,
            ViewLinks,
            Stop,
            Pause,
            Resume,
            FilterDiagram,
            ShowAllObjects,
            ShowApplications,
            ShowOtherObjects,
            ShowPrimRelationships,
            Expand,
            Collapse,
            Refresh,
            Layout,
            ShowResources,
            InsertHTMLWizard,
            ShowDownloads,
            ShowExternals,
            ShowInBoundLinks,
            ShowOutBoundLinks,
            ShowInAndOutBoundLinks,
            Preview,
            Open = 261,
            OpenWith = 199,
            ShowPages,
            RunQuery,
            ClearQuery,
            RecordFirst,
            RecordLast,
            RecordNext,
            RecordPrevious,
            RecordGoto,
            RecordNew,
            InsertNewMenu,
            InsertSeparator,
            EditMenuNames,
            DebugExplorer,
            DebugProcesses,
            ViewThreadsWindow,
            WindowUIList,
            NewProject,
            OpenProject,
            OpenProjectFromWeb = 450,
            OpenSolution = 218,
            CloseSolution,
            FileNew = 221,
            NewProjectFromExisting = 385,
            FileOpen = 222,
            FileOpenFromWeb = 451,
            FileClose = 223,
            SaveSolution,
            SaveSolutionAs,
            SaveProjectItemAs,
            PageSetup,
            PrintPreview,
            Exit,
            Replace,
            Goto,
            PropertyPages,
            FullScreen,
            ProjectExplorer,
            PropertiesWindow,
            TaskListWindow,
            OutputWindow,
            ObjectBrowser,
            DocOutlineWindow,
            ImmediateWindow,
            WatchWindow,
            LocalsWindow,
            CallStack,
            AutosWindow = 747,
            ThisWindow,
            AddNewItem = 220,
            AddExistingItem = 244,
            NewFolder,
            SetStartupProject,
            ProjectSettings,
            ProjectReferences = 367,
            StepInto = 248,
            StepOver,
            StepOut,
            RunToCursor,
            AddWatch,
            EditWatch,
            QuickWatch,
            ToggleBreakpoint,
            ClearBreakpoints,
            ShowBreakpoints,
            SetNextStatement,
            ShowNextStatement,
            EditBreakpoint,
            DetachDebugger = 262,
            CustomizeKeyboard,
            ToolsOptions,
            NewWindow,
            Split,
            Cascade,
            TileHorz,
            TileVert,
            TechSupport,
            About,
            DebugOptions,
            DeleteWatch = 274,
            CollapseWatch,
            PbrsToggleStatus = 282,
            PropbrsHide,
            DockingView,
            HideActivePane,
            PaneNextPane = 316,
            PanePrevPane,
            PaneNextTab = 286,
            PanePrevTab,
            PaneCloseToolWindow,
            PaneActivateDocWindow,
            DockingViewMDI,
            DockingViewFloater,
            AutoHideWindow,
            MoveToDropdownBar,
            FindCmd,
            Start,
            Restart,
            AddinManager,
            MultiLevelUndoList,
            MultiLevelRedoList,
            ToolboxAddTab,
            ToolboxDeleteTab,
            ToolboxRenameTab,
            ToolboxTabMoveUp,
            ToolboxTabMoveDown,
            ToolboxRenameItem,
            ToolboxListView,
            WindowUIGetList = 308,
            InsertValuesQuery,
            ShowProperties,
            ThreadSuspend,
            ThreadResume,
            ThreadSetFocus,
            DisplayRadix,
            OpenProjectItem,
            ClearPane = 318,
            GotoErrorTag,
            TaskListSortByCategory,
            TaskListSortByFileLine,
            TaskListSortByPriority,
            TaskListSortByDefaultSort,
            TaskListShowTooltip,
            TaskListFilterByNothing,
            CancelEZDrag,
            TaskListFilterByCategoryCompiler,
            TaskListFilterByCategoryComment,
            ToolboxAddItem,
            ToolboxReset,
            SaveProjectItem,
            SaveOptions = 959,
            ViewForm = 332,
            ViewCode,
            PreviewInBrowser,
            BrowseWith = 336,
            SearchSetCombo = 307,
            SearchCombo = 337,
            EditLabel,
            Exceptions,
            DefineViews,
            ToggleSelMode,
            ToggleInsMode,
            LoadUnloadedProject,
            UnloadLoadedProject,
            ElasticColumn,
            HideColumn,
            TaskListPreviousView,
            ZoomDialog,
            FindHiddenText,
            FindMatchCase,
            FindWholeWord,
            FindSimplePattern = 276,
            FindRegularExpression = 352,
            FindBackwards,
            FindInSelection,
            FindStop,
            FindInFiles = 277,
            ReplaceInFiles,
            NextLocation,
            PreviousLocation,
            GotoQuick,
            TaskListNextError = 357,
            TaskListPrevError,
            TaskListFilterByCategoryUser,
            TaskListFilterByCategoryShortcut,
            TaskListFilterByCategoryHTML,
            TaskListFilterByCurrentFile,
            TaskListFilterByChecked,
            TaskListFilterByUnchecked,
            TaskListSortByDescription,
            TaskListSortByChecked,
            StartNoDebug = 368,
            FindNext = 370,
            FindPrev,
            FindSelectedNext,
            FindSelectedPrev,
            SearchGetList,
            InsertBreakpoint,
            EnableBreakpoint,
            F1Help,
            MoveToNextEZCntr = 384,
            UpdateMarkerSpans = 386,
            MoveToPreviousEZCntr = 393,
            ProjectProperties = 396,
            PropSheetOrProperties,
            TshellStep,
            TshellRun,
            MarkerCmd0,
            MarkerCmd1,
            MarkerCmd2,
            MarkerCmd3,
            MarkerCmd4,
            MarkerCmd5,
            MarkerCmd6,
            MarkerCmd7,
            MarkerCmd8,
            MarkerCmd9,
            MarkerLast = 409,
            MarkerEnd,
            ReloadProject = 412,
            UnloadProject,
            NewBlankSolution,
            SelectProjectTemplate,
            DetachAttachOutline = 420,
            ShowHideOutline,
            SyncOutline,
            RunToCallstCursor,
            NoCmdsAvailable,
            ContextWindow = 427,
            Alias,
            GotoCommandLine,
            EvaluateExpression,
            ImmediateMode,
            EvaluateStatement,
            FindResultWindow1,
            FindResultWindow2,
            RenameBookmark = 559,
            ToggleBookmark,
            DeleteBookmark,
            BookmarkWindowGoToBookmark,
            EnableBookmark = 564,
            NewBookmarkFolder,
            NextBookmarkFolder = 568,
            PrevBookmarkFolder,
            Window1,
            Window2,
            Window3,
            Window4,
            Window5,
            Window6,
            Window7,
            Window8,
            Window9,
            Window10,
            Window11,
            Window12,
            Window13,
            Window14,
            Window15,
            Window16,
            Window17,
            Window18,
            Window19,
            Window20,
            Window21,
            Window22,
            Window23,
            Window24,
            Window25,
            MoreWindows,
            AutoHideAllWindows = 597,
            TaskListTaskHelp,
            ClassView,
            MRUProj1,
            MRUProj2,
            MRUProj3,
            MRUProj4,
            MRUProj5,
            MRUProj6,
            MRUProj7,
            MRUProj8,
            MRUProj9,
            MRUProj10,
            MRUProj11,
            MRUProj12,
            MRUProj13,
            MRUProj14,
            MRUProj15,
            MRUProj16,
            MRUProj17,
            MRUProj18,
            MRUProj19,
            MRUProj20,
            MRUProj21,
            MRUProj22,
            MRUProj23,
            MRUProj24,
            MRUProj25,
            SplitNext,
            SplitPrev,
            CloseAllDocuments,
            NextDocument,
            PrevDocument,
            Tool1,
            Tool2,
            Tool3,
            Tool4,
            Tool5,
            Tool6,
            Tool7,
            Tool8,
            Tool9,
            Tool10,
            Tool11,
            Tool12,
            Tool13,
            Tool14,
            Tool15,
            Tool16,
            Tool17,
            Tool18,
            Tool19,
            Tool20,
            Tool21,
            Tool22,
            Tool23,
            Tool24,
            ExternalCommands,
            PasteNextTBXCBItem,
            ToolboxShowAllTabs,
            ProjectDependencies,
            CloseDocument,
            ToolboxSortItems,
            ViewBarView1,
            ViewBarView2,
            ViewBarView3,
            ViewBarView4,
            ViewBarView5,
            ViewBarView6,
            ViewBarView7,
            ViewBarView8,
            ViewBarView9,
            ViewBarView10,
            ViewBarView11,
            ViewBarView12,
            ViewBarView13,
            ViewBarView14,
            ViewBarView15,
            ViewBarView16,
            ViewBarView17,
            ViewBarView18,
            ViewBarView19,
            ViewBarView20,
            ViewBarView21,
            ViewBarView22,
            ViewBarView23,
            ViewBarView24,
            SolutionCfg,
            SolutionCfgGetList,
            ManageIndexes = 675,
            ManageRelationships,
            ManageConstraints,
            TaskListCustomView1,
            TaskListCustomView2,
            TaskListCustomView3,
            TaskListCustomView4,
            TaskListCustomView5,
            TaskListCustomView6,
            TaskListCustomView7,
            TaskListCustomView8,
            TaskListCustomView9,
            TaskListCustomView10,
            TaskListCustomView11,
            TaskListCustomView12,
            TaskListCustomView13,
            TaskListCustomView14,
            TaskListCustomView15,
            TaskListCustomView16,
            TaskListCustomView17,
            TaskListCustomView18,
            TaskListCustomView19,
            TaskListCustomView20,
            TaskListCustomView21,
            TaskListCustomView22,
            TaskListCustomView23,
            TaskListCustomView24,
            TaskListCustomView25,
            TaskListCustomView26,
            TaskListCustomView27,
            TaskListCustomView28,
            TaskListCustomView29,
            TaskListCustomView30,
            TaskListCustomView31,
            TaskListCustomView32,
            TaskListCustomView33,
            TaskListCustomView34,
            TaskListCustomView35,
            TaskListCustomView36,
            TaskListCustomView37,
            TaskListCustomView38,
            TaskListCustomView39,
            TaskListCustomView40,
            TaskListCustomView41,
            TaskListCustomView42,
            TaskListCustomView43,
            TaskListCustomView44,
            TaskListCustomView45,
            TaskListCustomView46,
            TaskListCustomView47,
            TaskListCustomView48,
            TaskListCustomView49,
            TaskListCustomView50,
            WhiteSpace,
            CommandWindow,
            CommandWindowMarkMode,
            LogCommandWindow,
            Shell,
            SingleChar,
            ZeroOrMore,
            OneOrMore,
            BeginLine,
            EndLine,
            BeginWord,
            EndWord,
            CharInSet,
            CharNotInSet,
            Or,
            Escape,
            TagExp,
            PatternMatchHelp,
            RegExList,
            DebugReserved1,
            DebugReserved2,
            DebugReserved3,
            WildZeroOrMore = 754,
            WildSingleChar,
            WildSingleDigit,
            WildCharInSet,
            WildCharNotInSet,
            FindWhatText,
            TaggedExp1,
            TaggedExp2,
            TaggedExp3,
            TaggedExp4,
            TaggedExp5,
            TaggedExp6,
            TaggedExp7,
            TaggedExp8,
            TaggedExp9,
            EditorWidgetClick,
            CmdWinUpdateAC,
            SlnCfgMgr,
            AddNewProject,
            AddExistingProject,
            AddExistingProjFromWeb,
            AutoHideContext1 = 776,
            AutoHideContext2,
            AutoHideContext3,
            AutoHideContext4,
            AutoHideContext5,
            AutoHideContext6,
            AutoHideContext7,
            AutoHideContext8,
            AutoHideContext9,
            AutoHideContext10,
            AutoHideContext11,
            AutoHideContext12,
            AutoHideContext13,
            AutoHideContext14,
            AutoHideContext15,
            AutoHideContext16,
            AutoHideContext17,
            AutoHideContext18,
            AutoHideContext19,
            AutoHideContext20,
            AutoHideContext21,
            AutoHideContext22,
            AutoHideContext23,
            AutoHideContext24,
            AutoHideContext25,
            AutoHideContext26,
            AutoHideContext27,
            AutoHideContext28,
            AutoHideContext29,
            AutoHideContext30,
            AutoHideContext31,
            AutoHideContext32,
            AutoHideContext33,
            ShellNavBackward,
            ShellNavForward,
            ShellNavigate1,
            ShellNavigate2,
            ShellNavigate3,
            ShellNavigate4,
            ShellNavigate5,
            ShellNavigate6,
            ShellNavigate7,
            ShellNavigate8,
            ShellNavigate9,
            ShellNavigate10,
            ShellNavigate11,
            ShellNavigate12,
            ShellNavigate13,
            ShellNavigate14,
            ShellNavigate15,
            ShellNavigate16,
            ShellNavigate17,
            ShellNavigate18,
            ShellNavigate19,
            ShellNavigate20,
            ShellNavigate21,
            ShellNavigate22,
            ShellNavigate23,
            ShellNavigate24,
            ShellNavigate25,
            ShellNavigate26,
            ShellNavigate27,
            ShellNavigate28,
            ShellNavigate29,
            ShellNavigate30,
            ShellNavigate31,
            ShellNavigate32,
            ShellNavigate33,
            ShellWindowNavigate1,
            ShellWindowNavigate2,
            ShellWindowNavigate3,
            ShellWindowNavigate4,
            ShellWindowNavigate5,
            ShellWindowNavigate6,
            ShellWindowNavigate7,
            ShellWindowNavigate8,
            ShellWindowNavigate9,
            ShellWindowNavigate10,
            ShellWindowNavigate11,
            ShellWindowNavigate12,
            ShellWindowNavigate13,
            ShellWindowNavigate14,
            ShellWindowNavigate15,
            ShellWindowNavigate16,
            ShellWindowNavigate17,
            ShellWindowNavigate18,
            ShellWindowNavigate19,
            ShellWindowNavigate20,
            ShellWindowNavigate21,
            ShellWindowNavigate22,
            ShellWindowNavigate23,
            ShellWindowNavigate24,
            ShellWindowNavigate25,
            ShellWindowNavigate26,
            ShellWindowNavigate27,
            ShellWindowNavigate28,
            ShellWindowNavigate29,
            ShellWindowNavigate30,
            ShellWindowNavigate31,
            ShellWindowNavigate32,
            ShellWindowNavigate33,
            OBSDoFind,
            OBSMatchCase,
            OBSMatchSubString,
            OBSMatchWholeWord,
            OBSMatchPrefix,
            BuildSln,
            RebuildSln,
            DeploySln,
            CleanSln,
            BuildSel,
            RebuildSel,
            DeploySel,
            CleanSel,
            CancelBuild,
            BatchBuildDlg,
            BuildCtx,
            RebuildCtx,
            DeployCtx,
            CleanCtx,
            QryManageIndexes,
            PrintDefault,
            BrowseDoc,
            ShowStartPage,
            MRUFile1,
            MRUFile2,
            MRUFile3,
            MRUFile4,
            MRUFile5,
            MRUFile6,
            MRUFile7,
            MRUFile8,
            MRUFile9,
            MRUFile10,
            MRUFile11,
            MRUFile12,
            MRUFile13,
            MRUFile14,
            MRUFile15,
            MRUFile16,
            MRUFile17,
            MRUFile18,
            MRUFile19,
            MRUFile20,
            MRUFile21,
            MRUFile22,
            MRUFile23,
            MRUFile24,
            MRUFile25,
            ExtToolsCurPath,
            ExtToolsCurDir,
            ExtToolsCurFileName,
            ExtToolsCurExtension,
            ExtToolsProjDir,
            ExtToolsProjFileName,
            ExtToolsSlnDir,
            ExtToolsSlnFileName,
            GotoDefn = 935,
            GotoDecl,
            BrowseDefn,
            SyncClassView,
            ShowMembers,
            ShowBases,
            ShowDerived,
            ShowDefns,
            ShowRefs,
            ShowCallers,
            ShowCallees,
            AddClass,
            AddNestedClass,
            AddInterface,
            AddMethod,
            AddProperty,
            AddEvent,
            AddVariable,
            ImplementInterface,
            Override,
            AddFunction,
            AddConnectionPoint,
            AddIndexer,
            BuildOrder,
            OBShowHidden = 960,
            OBEnableGrouping,
            OBSetGroupingCriteria,
            OBBack,
            OBForward,
            OBShowPackages,
            OBSearchCombo,
            OBSearchOptWholeWord,
            OBSearchOptSubstring,
            OBSearchOptPrefix,
            OBSearchOptCaseSensitive,
            CVGroupingNone,
            CVGroupingSortOnly,
            CVGroupingGrouped,
            CVShowPackages,
            CVNewFolder,
            CVGroupingSortAccess,
            ObjectSearch,
            ObjectSearchResults,
            Build1,
            Build2,
            Build3,
            Build4,
            Build5,
            Build6,
            Build7,
            Build8,
            Build9,
            BuildLast,
            Rebuild1,
            Rebuild2,
            Rebuild3,
            Rebuild4,
            Rebuild5,
            Rebuild6,
            Rebuild7,
            Rebuild8,
            Rebuild9,
            RebuildLast,
            Clean1,
            Clean2,
            Clean3,
            Clean4,
            Clean5,
            Clean6,
            Clean7,
            Clean8,
            Clean9,
            CleanLast,
            Deploy1,
            Deploy2,
            Deploy3,
            Deploy4,
            Deploy5,
            Deploy6,
            Deploy7,
            Deploy8,
            Deploy9,
            DeployLast,
            BuildProjPicker,
            RebuildProjPicker,
            CleanProjPicker,
            DeployProjPicker,
            ResourceView,
            ShowHomePage,
            EditMenuIDs,
            LineBreak,
            CPPIdentifier,
            QuotedString,
            SpaceOrTab,
            Integer,
            CustomizeToolbars = 1036,
            MoveToTop,
            WindowHelp,
            ViewPopup,
            CheckMnemonics,
            PRSortAlphabeticaly,
            PRSortByCategory,
            ViewNextTab,
            CheckForUpdates,
            Browser1,
            Browser2,
            Browser3,
            Browser4,
            Browser5,
            Browser6,
            Browser7,
            Browser8,
            Browser9,
            Browser10,
            Browser11,
            OpenDropDownOpen = 1058,
            OpenDropDownOpenWith,
            ToolsDebugProcesses,
            PaneNextSubPane = 1062,
            PanePrevSubPane,
            MoveFileToProject1 = 1070,
            MoveFileToProject2,
            MoveFileToProject3,
            MoveFileToProject4,
            MoveFileToProject5,
            MoveFileToProject6,
            MoveFileToProject7,
            MoveFileToProject8,
            MoveFileToProject9,
            MoveFileToProjectLast,
            MoveFileToProjectPick = 1081,
            DefineSubset = 1095,
            SubsetCombo,
            SubsetGetList,
            OBSortObjectsAlpha,
            OBSortObjectsType,
            OBSortObjectsAccess,
            OBGroupObjectsType,
            OBGroupObjectsAccess,
            OBSortMembersAlpha,
            OBSortMembersType,
            OBSortMembersAccess,
            PopBrowseContext,
            GotoRef,
            OBSLookInReferences,
            ExtToolsTargetPath,
            ExtToolsTargetDir,
            ExtToolsTargetFileName,
            ExtToolsTargetExtension,
            ExtToolsCurLine,
            ExtToolsCurCol,
            ExtToolsCurText,
            BrowseNext,
            BrowsePrev,
            BrowseUnload,
            QuickObjectSearch,
            ExpandAll,
            ExtToolsBinDir,
            BookmarkWindow,
            CodeExpansionWindow,
            NextDocumentNav,
            PrevDocumentNav,
            ForwardBrowseContext,
            StandardMax = 1500,
            FindReferences = 1915,
            FormsFirst = 24576,
            FormsLast = 28671,
            VBEFirst = 32768,
            Zoom200 = 32770,
            Zoom150,
            Zoom100,
            Zoom75,
            Zoom50,
            Zoom25,
            Zoom10 = 32784,
            VBELast = 40959,
            SterlingFirst,
            SterlingLast = 49151,
            uieventidFirst,
            uieventidSelectRegion,
            uieventidDrop,
            uieventidLast = 57343
        }

        [Guid("1496A755-94DE-11D0-8C3F-00C04FC2AAE2")]
        public enum VSStd2KCmdID
        {
            TYPECHAR = 1,
            BACKSPACE,
            RETURN,
            TAB,
            ECMD_TAB = 4,
            BACKTAB,
            DELETE,
            LEFT,
            LEFT_EXT,
            RIGHT,
            RIGHT_EXT,
            UP,
            UP_EXT,
            DOWN,
            DOWN_EXT,
            HOME,
            HOME_EXT,
            END,
            END_EXT,
            BOL,
            BOL_EXT,
            FIRSTCHAR,
            FIRSTCHAR_EXT,
            EOL,
            EOL_EXT,
            LASTCHAR,
            LASTCHAR_EXT,
            PAGEUP,
            PAGEUP_EXT,
            PAGEDN,
            PAGEDN_EXT,
            TOPLINE,
            TOPLINE_EXT,
            BOTTOMLINE,
            BOTTOMLINE_EXT,
            SCROLLUP,
            SCROLLDN,
            SCROLLPAGEUP,
            SCROLLPAGEDN,
            SCROLLLEFT,
            SCROLLRIGHT,
            SCROLLBOTTOM,
            SCROLLCENTER,
            SCROLLTOP,
            SELECTALL,
            SELTABIFY,
            SELUNTABIFY,
            SELLOWCASE,
            SELUPCASE,
            SELTOGGLECASE,
            SELTITLECASE,
            SELSWAPANCHOR,
            GOTOLINE,
            GOTOBRACE,
            GOTOBRACE_EXT,
            GOBACK,
            SELECTMODE,
            TOGGLE_OVERTYPE_MODE,
            CUT,
            COPY,
            PASTE,
            CUTLINE,
            DELETELINE,
            DELETEBLANKLINES,
            DELETEWHITESPACE,
            DELETETOEOL,
            DELETETOBOL,
            OPENLINEABOVE,
            OPENLINEBELOW,
            INDENT,
            UNINDENT,
            UNDO,
            UNDONOMOVE,
            REDO,
            REDONOMOVE,
            DELETEALLTEMPBOOKMARKS,
            TOGGLETEMPBOOKMARK,
            GOTONEXTBOOKMARK,
            GOTOPREVBOOKMARK,
            FIND,
            REPLACE,
            REPLACE_ALL,
            FINDNEXT,
            FINDNEXTWORD,
            FINDPREV,
            FINDPREVWORD,
            FINDAGAIN,
            TRANSPOSECHAR,
            TRANSPOSEWORD,
            TRANSPOSELINE,
            SELECTCURRENTWORD,
            DELETEWORDRIGHT,
            DELETEWORDLEFT,
            WORDPREV,
            WORDPREV_EXT,
            WORDNEXT = 96,
            WORDNEXT_EXT,
            COMMENTBLOCK,
            UNCOMMENTBLOCK,
            SETREPEATCOUNT,
            WIDGETMARGIN_LBTNDOWN,
            SHOWCONTEXTMENU,
            CANCEL,
            PARAMINFO,
            TOGGLEVISSPACE,
            TOGGLECARETPASTEPOS,
            COMPLETEWORD,
            SHOWMEMBERLIST,
            FIRSTNONWHITEPREV,
            FIRSTNONWHITENEXT,
            HELPKEYWORD,
            FORMATSELECTION,
            OPENURL,
            INSERTFILE,
            TOGGLESHORTCUT,
            QUICKINFO,
            LEFT_EXT_COL,
            RIGHT_EXT_COL,
            UP_EXT_COL,
            DOWN_EXT_COL,
            TOGGLEWORDWRAP,
            ISEARCH,
            ISEARCHBACK,
            BOL_EXT_COL,
            EOL_EXT_COL,
            WORDPREV_EXT_COL,
            WORDNEXT_EXT_COL,
            OUTLN_HIDE_SELECTION,
            OUTLN_TOGGLE_CURRENT,
            OUTLN_TOGGLE_ALL,
            OUTLN_STOP_HIDING_ALL,
            OUTLN_STOP_HIDING_CURRENT,
            OUTLN_COLLAPSE_TO_DEF,
            DOUBLECLICK,
            EXTERNALLY_HANDLED_WIDGET_CLICK,
            COMMENT_BLOCK,
            UNCOMMENT_BLOCK,
            OPENFILE,
            NAVIGATETOURL,
            HANDLEIMEMESSAGE,
            SELTOGOBACK,
            COMPLETION_HIDE_ADVANCED,
            FORMATDOCUMENT,
            OUTLN_START_AUTOHIDING,
            FINAL,
            ECMD_DECREASEFILTER,
            ECMD_COPYTIP = 148,
            ECMD_PASTETIP,
            ECMD_LEFTCLICK,
            ECMD_GOTONEXTBOOKMARKINDOC,
            ECMD_GOTOPREVBOOKMARKINDOC,
            ECMD_INVOKESNIPPETFROMSHORTCUT = 154,
            AUTOCOMPLETE,
            ECMD_INVOKESNIPPETPICKER2,
            ECMD_DELETEALLBOOKMARKSINDOC,
            ECMD_CONVERTTABSTOSPACES,
            ECMD_CONVERTSPACESTOTABS,
            ECMD_FINAL,
            STOP = 220,
            REVERSECANCEL,
            SLNREFRESH,
            SAVECOPYOFITEMAS,
            NEWELEMENT,
            NEWATTRIBUTE,
            NEWCOMPLEXTYPE,
            NEWSIMPLETYPE,
            NEWGROUP,
            NEWATTRIBUTEGROUP,
            NEWKEY,
            NEWRELATION,
            EDITKEY,
            EDITRELATION,
            MAKETYPEGLOBAL,
            PREVIEWDATASET,
            GENERATEDATASET,
            CREATESCHEMA,
            LAYOUTINDENT,
            LAYOUTUNINDENT,
            REMOVEHANDLER,
            EDITHANDLER,
            ADDHANDLER,
            STYLE,
            STYLEGETLIST,
            FONTSTYLE,
            FONTSTYLEGETLIST,
            PASTEASHTML,
            VIEWBORDERS,
            VIEWDETAILS,
            EXPANDCONTROLS,
            COLLAPSECONTROLS,
            SHOWSCRIPTONLY,
            INSERTTABLE,
            INSERTCOLLEFT,
            INSERTCOLRIGHT,
            INSERTROWABOVE,
            INSERTROWBELOW,
            DELETETABLE,
            DELETECOLS,
            DELETEROWS,
            SELECTTABLE,
            SELECTTABLECOL,
            SELECTTABLEROW,
            SELECTTABLECELL,
            MERGECELLS,
            SPLITCELL,
            INSERTCELL,
            DELETECELLS,
            SEAMLESSFRAME,
            VIEWFRAME,
            DELETEFRAME,
            SETFRAMESOURCE,
            NEWLEFTFRAME,
            NEWRIGHTFRAME,
            NEWTOPFRAME,
            NEWBOTTOMFRAME,
            SHOWGRID,
            SNAPTOGRID,
            BOOKMARK,
            HYPERLINK,
            IMAGE,
            INSERTFORM,
            INSERTSPAN,
            DIV,
            HTMLCLIENTSCRIPTBLOCK,
            HTMLSERVERSCRIPTBLOCK,
            BULLETEDLIST,
            NUMBEREDLIST,
            EDITSCRIPT,
            EDITCODEBEHIND,
            DOCOUTLINEHTML,
            DOCOUTLINESCRIPT,
            RUNATSERVER,
            WEBFORMSVERBS,
            WEBFORMSTEMPLATES,
            ENDTEMPLATE,
            EDITDEFAULTEVENT,
            SUPERSCRIPT,
            SUBSCRIPT,
            EDITSTYLE,
            ADDIMAGEHEIGHTWIDTH,
            REMOVEIMAGEHEIGHTWIDTH,
            LOCKELEMENT,
            VIEWSTYLEORGANIZER,
            ECMD_AUTOCLOSEOVERRIDE,
            NEWANY,
            NEWANYATTRIBUTE,
            DELETEKEY,
            AUTOARRANGE,
            VALIDATESCHEMA,
            NEWFACET,
            VALIDATEXMLDATA,
            DOCOUTLINETOGGLE,
            VALIDATEHTMLDATA,
            VIEWXMLSCHEMAOVERVIEW,
            SHOWDEFAULTVIEW,
            EXPAND_CHILDREN,
            COLLAPSE_CHILDREN,
            TOPDOWNLAYOUT,
            LEFTRIGHTLAYOUT,
            INSERTCELLRIGHT,
            EDITMASTER,
            INSERTSNIPPET,
            FORMATANDVALIDATION,
            COLLAPSETAG,
            SELECT_TAG = 329,
            SELECT_TAG_CONTENT,
            CHECK_ACCESSIBILITY,
            UNCOLLAPSETAG,
            GENERATEPAGERESOURCE,
            SHOWNONVISUALCONTROLS,
            RESIZECOLUMN,
            RESIZEROW,
            MAKEABSOLUTE,
            MAKERELATIVE,
            MAKESTATIC,
            INSERTLAYER,
            UPDATEDESIGNVIEW,
            UPDATESOURCEVIEW,
            INSERTCAPTION,
            DELETECAPTION,
            MAKEPOSITIONNOTSET,
            AUTOPOSITIONOPTIONS,
            EDITIMAGE,
            COMPILE = 350,
            PROJSETTINGS = 352,
            LINKONLY,
            REMOVE = 355,
            PROJSTARTDEBUG,
            PROJSTEPINTO,
            ECMD_UPDATEMGDRES,
            UPDATEWEBREF = 360,
            ADDRESOURCE = 362,
            WEBDEPLOY,
            ECMD_PROJTOOLORDER = 367,
            ECMD_PROJECTTOOLFILES,
            ECMD_OTB_PGO_INSTRUMENT,
            ECMD_OTB_PGO_OPT,
            ECMD_OTB_PGO_UPDATE,
            ECMD_OTB_PGO_RUNSCENARIO,
            ADDHTMLPAGE = 400,
            ADDHTMLPAGECTX,
            ADDMODULE,
            ADDMODULECTX,
            ADDWFCFORM = 406,
            ADDWEBFORM = 410,
            ECMD_ADDMASTERPAGE,
            ADDUSERCONTROL,
            ECMD_ADDCONTENTPAGE,
            ADDDHTMLPAGE = 426,
            ADDIMAGEGENERATOR = 432,
            ADDINHERWFCFORM = 434,
            ADDINHERCONTROL = 436,
            ADDWEBUSERCONTROL = 438,
            BUILDANDBROWSE,
            ADDTBXCOMPONENT = 442,
            ADDWEBSERVICE = 444,
            ECMD_ADDSTYLESHEET,
            ECMD_SETBROWSELOCATION,
            ECMD_REFRESHFOLDER,
            ECMD_SETBROWSELOCATIONCTX,
            ECMD_VIEWMARKUP,
            ECMD_NEXTMETHOD,
            ECMD_PREVMETHOD,
            ECMD_RENAMESYMBOL,
            ECMD_SHOWREFERENCES,
            ECMD_CREATESNIPPET,
            ECMD_CREATEREPLACEMENT,
            ECMD_INSERTCOMMENT,
            VIEWCOMPONENTDESIGNER,
            GOTOTYPEDEF,
            SHOWSNIPPETHIGHLIGHTING,
            HIDESNIPPETHIGHLIGHTING,
            ADDVFPPAGE = 500,
            SETBREAKPOINT,
            SHOWALLFILES = 600,
            ADDTOPROJECT,
            ADDBLANKNODE,
            ADDNODEFROMFILE,
            CHANGEURLFROMFILE,
            EDITTOPIC,
            EDITTITLE,
            MOVENODEUP,
            MOVENODEDOWN,
            MOVENODELEFT,
            MOVENODERIGHT,
            ADDOUTPUT = 700,
            ADDFILE,
            MERGEMODULE,
            ADDCOMPONENTS,
            LAUNCHINSTALLER,
            LAUNCHUNINSTALL,
            LAUNCHORCA,
            FILESYSTEMEDITOR,
            REGISTRYEDITOR,
            FILETYPESEDITOR,
            USERINTERFACEEDITOR,
            CUSTOMACTIONSEDITOR,
            LAUNCHCONDITIONSEDITOR,
            EDITOR,
            EXCLUDE,
            REFRESHDEPENDENCIES,
            VIEWOUTPUTS,
            VIEWDEPENDENCIES,
            VIEWFILTER,
            KEY = 750,
            STRING,
            BINARY,
            DWORD,
            KEYSOLO,
            IMPORT,
            FOLDER,
            PROJECTOUTPUT,
            FILE,
            ADDMERGEMODULES,
            CREATESHORTCUT,
            LARGEICONS,
            SMALLICONS,
            LIST,
            DETAILS,
            ADDFILETYPE,
            ADDACTION,
            SETASDEFAULT,
            MOVEUP,
            MOVEDOWN,
            ADDDIALOG,
            IMPORTDIALOG,
            ADDFILESEARCH,
            ADDREGISTRYSEARCH,
            ADDCOMPONENTSEARCH,
            ADDLAUNCHCONDITION,
            ADDCUSTOMACTION,
            OUTPUTS,
            DEPENDENCIES,
            FILTER,
            COMPONENTS,
            ENVSTRING,
            CREATEEMPTYSHORTCUT,
            ADDFILECONDITION,
            ADDREGISTRYCONDITION,
            ADDCOMPONENTCONDITION,
            ADDURTCONDITION,
            ADDIISCONDITION,
            SPECIALFOLDERBASE = 800,
            USERSAPPLICATIONDATAFOLDER = 800,
            COMMONFILES64FOLDER,
            COMMONFILESFOLDER,
            CUSTOMFOLDER,
            USERSDESKTOP,
            USERSFAVORITESFOLDER,
            FONTSFOLDER,
            GLOBALASSEMBLYCACHEFOLDER,
            MODULERETARGETABLEFOLDER,
            USERSPERSONALDATAFOLDER,
            PROGRAMFILES64FOLDER,
            PROGRAMFILESFOLDER,
            USERSPROGRAMSMENU,
            USERSSENDTOMENU,
            SHAREDCOMPONENTSFOLDER,
            USERSSTARTMENU,
            USERSSTARTUPFOLDER,
            SYSTEM64FOLDER,
            SYSTEMFOLDER,
            APPLICATIONFOLDER,
            USERSTEMPLATEFOLDER,
            WEBCUSTOMFOLDER,
            WINDOWSFOLDER,
            SPECIALFOLDERLAST,
            EXPORTEVENTS = 900,
            IMPORTEVENTS,
            VIEWEVENT,
            VIEWEVENTLIST,
            VIEWCHART,
            VIEWMACHINEDIAGRAM,
            VIEWPROCESSDIAGRAM,
            VIEWSOURCEDIAGRAM,
            VIEWSTRUCTUREDIAGRAM,
            VIEWTIMELINE,
            VIEWSUMMARY,
            APPLYFILTER,
            CLEARFILTER,
            STARTRECORDING,
            STOPRECORDING,
            PAUSERECORDING,
            ACTIVATEFILTER,
            SHOWFIRSTEVENT,
            SHOWPREVIOUSEVENT,
            SHOWNEXTEVENT,
            SHOWLASTEVENT,
            REPLAYEVENTS,
            STOPREPLAY,
            INCREASEPLAYBACKSPEED,
            DECREASEPLAYBACKSPEED,
            ADDMACHINE,
            ADDREMOVECOLUMNS,
            SORTCOLUMNS,
            SAVECOLUMNSETTINGS,
            RESETCOLUMNSETTINGS,
            SIZECOLUMNSTOFIT,
            AUTOSELECT,
            AUTOFILTER,
            AUTOPLAYTRACK,
            GOTOEVENT,
            ZOOMTOFIT,
            ADDGRAPH,
            REMOVEGRAPH,
            CONNECTMACHINE,
            DISCONNECTMACHINE,
            EXPANDSELECTION,
            COLLAPSESELECTION,
            ADDFILTER,
            ADDPREDEFINED0,
            ADDPREDEFINED1,
            ADDPREDEFINED2,
            ADDPREDEFINED3,
            ADDPREDEFINED4,
            ADDPREDEFINED5,
            ADDPREDEFINED6,
            ADDPREDEFINED7,
            ADDPREDEFINED8,
            TIMELINESIZETOFIT,
            FIELDVIEW = 1000,
            SELECTEXPERT,
            TOPNEXPERT,
            SORTORDER,
            PROPPAGE,
            HELP,
            SAVEREPORT,
            INSERTSUMMARY,
            INSERTGROUP,
            INSERTSUBREPORT,
            INSERTCHART,
            INSERTPICTURE,
            SETASSTARTPAGE = 1100,
            RECALCULATELINKS,
            WEBPERMISSIONS,
            COMPARETOMASTER,
            WORKOFFLINE,
            SYNCHRONIZEFOLDER,
            SYNCHRONIZEALLFOLDERS,
            COPYPROJECT,
            IMPORTFILEFROMWEB,
            INCLUDEINPROJECT,
            EXCLUDEFROMPROJECT,
            BROKENLINKSREPORT,
            ADDPROJECTOUTPUTS,
            ADDREFERENCE,
            ADDWEBREFERENCE,
            ADDWEBREFERENCECTX,
            UPDATEWEBREFERENCE,
            RUNCUSTOMTOOL,
            SETRUNTIMEVERSION,
            VIEWREFINOBJECTBROWSER,
            PUBLISH,
            PUBLISHCTX,
            STARTOPTIONS = 1124,
            ADDREFERENCECTX,
            STARTOPTIONSCTX = 1127,
            DETACHLOCALDATAFILECTX,
            ADDSERVICEREFERENCE,
            ADDSERVICEREFERENCECTX,
            UPDATESERVICEREFERENCE,
            CONFIGURESERVICEREFERENCE,
            DRAG_MOVE = 1140,
            DRAG_COPY,
            DRAG_CANCEL,
            TESTDIALOG = 1200,
            SPACEACROSS,
            SPACEDOWN,
            TOGGLEGRID,
            TOGGLEGUIDES,
            SIZETOTEXT,
            CENTERVERT,
            CENTERHORZ,
            FLIPDIALOG,
            SETTABORDER,
            BUTTONRIGHT,
            BUTTONBOTTOM,
            AUTOLAYOUTGROW,
            AUTOLAYOUTNORESIZE,
            AUTOLAYOUTOPTIMIZE,
            GUIDESETTINGS,
            RESOURCEINCLUDES,
            RESOURCESYMBOLS,
            OPENBINARY,
            RESOURCEOPEN,
            RESOURCENEW,
            RESOURCENEWCOPY,
            INSERT,
            EXPORT,
            CTLMOVELEFT,
            CTLMOVEDOWN,
            CTLMOVERIGHT,
            CTLMOVEUP,
            CTLSIZEDOWN,
            CTLSIZEUP,
            CTLSIZELEFT,
            CTLSIZERIGHT,
            NEWACCELERATOR,
            CAPTUREKEYSTROKE,
            INSERTACTIVEXCTL,
            INVERTCOLORS,
            FLIPHORIZONTAL,
            FLIPVERTICAL,
            ROTATE90,
            SHOWCOLORSWINDOW,
            NEWSTRING,
            NEWINFOBLOCK,
            DELETEINFOBLOCK,
            ADJUSTCOLORS,
            LOADPALETTE,
            SAVEPALETTE,
            CHECKMNEMONICS,
            DRAWOPAQUE,
            TOOLBAREDITOR,
            GRIDSETTINGS,
            NEWDEVICEIMAGE,
            OPENDEVICEIMAGE,
            DELETEDEVICEIMAGE,
            VIEWASPOPUP,
            CHECKMENUMNEMONICS,
            SHOWIMAGEGRID,
            SHOWTILEGRID,
            MAGNIFY,
            ResProps,
            IMPORTICONIMAGE,
            EXPORTICONIMAGE,
            OPENEXTERNALEDITOR,
            PICKRECTANGLE = 1300,
            PICKREGION,
            PICKCOLOR,
            ERASERTOOL,
            FILLTOOL,
            PENCILTOOL,
            BRUSHTOOL,
            AIRBRUSHTOOL,
            LINETOOL,
            CURVETOOL,
            TEXTTOOL,
            RECTTOOL,
            OUTLINERECTTOOL,
            FILLEDRECTTOOL,
            ROUNDRECTTOOL,
            OUTLINEROUNDRECTTOOL,
            FILLEDROUNDRECTTOOL,
            ELLIPSETOOL,
            OUTLINEELLIPSETOOL,
            FILLEDELLIPSETOOL,
            SETHOTSPOT,
            ZOOMTOOL,
            ZOOM1X,
            ZOOM2X,
            ZOOM6X,
            ZOOM8X,
            TRANSPARENTBCKGRND,
            OPAQUEBCKGRND,
            ERASERSMALL,
            ERASERMEDIUM,
            ERASERLARGE,
            ERASERLARGER,
            CIRCLELARGE,
            CIRCLEMEDIUM,
            CIRCLESMALL,
            SQUARELARGE,
            SQUAREMEDIUM,
            SQUARESMALL,
            LEFTDIAGLARGE,
            LEFTDIAGMEDIUM,
            LEFTDIAGSMALL,
            RIGHTDIAGLARGE,
            RIGHTDIAGMEDIUM,
            RIGHTDIAGSMALL,
            SPLASHSMALL,
            SPLASHMEDIUM,
            SPLASHLARGE,
            LINESMALLER,
            LINESMALL,
            LINEMEDIUM,
            LINELARGE,
            LINELARGER,
            LARGERBRUSH,
            LARGEBRUSH,
            STDBRUSH,
            SMALLBRUSH,
            SMALLERBRUSH,
            ZOOMIN,
            ZOOMOUT,
            PREVCOLOR,
            PREVECOLOR,
            NEXTCOLOR,
            NEXTECOLOR,
            IMG_OPTIONS,
            STARTWEBADMINTOOL = 1400,
            NESTRELATEDFILES,
            CANCELDRAG = 1500,
            DEFAULTACTION,
            CTLMOVEUPGRID,
            CTLMOVEDOWNGRID,
            CTLMOVELEFTGRID,
            CTLMOVERIGHTGRID,
            CTLSIZERIGHTGRID,
            CTLSIZEUPGRID,
            CTLSIZELEFTGRID,
            CTLSIZEDOWNGRID,
            NEXTCTL,
            PREVCTL,
            RENAME = 1550,
            EXTRACTMETHOD,
            ENCAPSULATEFIELD,
            EXTRACTINTERFACE,
            PROMOTELOCAL,
            REMOVEPARAMETERS,
            REORDERPARAMETERS,
            GENERATEMETHODSTUB,
            IMPLEMENTINTERFACEIMPLICIT,
            IMPLEMENTINTERFACEEXPLICIT,
            IMPLEMENTABSTRACTCLASS,
            SURROUNDWITH,
            QUICKOBJECTSEARCH = 1119,
            ToggleWordWrapOW = 1600,
            GotoNextLocationOW,
            GotoPrevLocationOW,
            BuildOnlyProject,
            RebuildOnlyProject,
            CleanOnlyProject,
            SetBuildStartupsOnlyOnRun,
            UnhideAll,
            HideFolder,
            UnhideFolders,
            CopyFullPathName,
            SaveFolderAsSolution,
            ManageUserSettings,
            NewSolutionFolder,
            ClearPaneOW = 1615,
            GotoErrorTagOW,
            GotoNextErrorTagOW,
            GotoPrevErrorTagOW,
            ClearPaneFR1,
            GotoErrorTagFR1,
            GotoNextErrorTagFR1,
            GotoPrevErrorTagFR1,
            ClearPaneFR2,
            GotoErrorTagFR2,
            GotoNextErrorTagFR2,
            GotoPrevErrorTagFR2,
            OutputPaneCombo,
            OutputPaneComboList,
            DisableDockingChanges,
            ToggleFloat,
            ResetLayout,
            NewSolutionFolderBar = 1638,
            DataShortcut,
            NextToolWindow,
            PrevToolWindow,
            BrowseToFileInExplorer,
            ShowEzMDIFileMenu,
            PrevToolWindowNav = 1645,
            StaticAnalysisOnlyProject,
            ECMD_RUNFXCOPSEL,
            CloseAllButThis = 1650,
            CVShowInheritedMembers,
            CVShowBaseTypes,
            CVShowDerivedTypes,
            CVShowHidden,
            CVBack,
            CVForward,
            CVSearchCombo,
            CVSearch,
            CVSortObjectsAlpha,
            CVSortObjectsType,
            CVSortObjectsAccess,
            CVGroupObjectsType,
            CVSortMembersAlpha,
            CVSortMembersType,
            CVSortMembersAccess,
            CVTypeBrowserSettings,
            CVViewMembersAsImplementor,
            CVViewMembersAsSubclass,
            CVViewMembersAsUser,
            CVReserved1,
            CVReserved2,
            CVShowProjectReferences,
            CVGroupMembersType,
            CVClearSearch,
            CVFilterToType,
            CVSortByBestMatch,
            CVSearchMRUList,
            CVViewOtherMembers,
            CVSearchCmd,
            CVGoToSearchCmd,
            ControlGallery = 1700,
            OBShowInheritedMembers = 1711,
            OBShowBaseTypes,
            OBShowDerivedTypes,
            OBShowHidden,
            OBBack,
            OBForward,
            OBSearchCombo,
            OBSearch,
            OBSortObjectsAlpha,
            OBSortObjectsType,
            OBSortObjectsAccess,
            OBGroupObjectsType,
            OBSortMembersAlpha,
            OBSortMembersType,
            OBSortMembersAccess,
            OBTypeBrowserSettings,
            OBViewMembersAsImplementor,
            OBViewMembersAsSubclass,
            OBViewMembersAsUser,
            OBNamespacesView,
            OBContainersView,
            OBReserved1,
            OBGroupMembersType,
            OBClearSearch,
            OBFilterToType,
            OBSortByBestMatch,
            OBSearchMRUList,
            OBViewOtherMembers,
            OBSearchCmd,
            OBGoToSearchCmd,
            OBShowExtensionMembers,
            FullScreen2 = 1775,
            FSRSortObjectsAlpha,
            FSRSortByBestMatch,
            NavigateBack = 1800,
            NavigateForward,
            ECMD_CORRECTION_1 = 1900,
            ECMD_CORRECTION_2,
            ECMD_CORRECTION_3,
            ECMD_CORRECTION_4,
            ECMD_CORRECTION_5,
            ECMD_CORRECTION_6,
            ECMD_CORRECTION_7,
            ECMD_CORRECTION_8,
            ECMD_CORRECTION_9,
            ECMD_CORRECTION_10,
            OBAddReference = 1914,
            [Obsolete("VSStd2KCmdID.FindReferences has been deprecated; please use VSStd97CmdID.FindReferences instead.", false)]
            FindReferences,
            CodeDefView = 1926,
            CodeDefViewGoToPrev,
            CodeDefViewGoToNext,
            CodeDefViewEditDefinition,
            CodeDefViewChooseEncoding,
            ViewInClassDiagram,
            ECMD_ADDDBTABLE = 1950,
            ECMD_ADDDATATABLE,
            ECMD_ADDFUNCTION,
            ECMD_ADDRELATION,
            ECMD_ADDKEY,
            ECMD_ADDCOLUMN,
            ECMD_CONVERT_DBTABLE,
            ECMD_CONVERT_DATATABLE,
            ECMD_GENERATE_DATABASE,
            ECMD_CONFIGURE_CONNECTIONS,
            ECMD_IMPORT_XMLSCHEMA,
            ECMD_SYNC_WITH_DATABASE,
            ECMD_CONFIGURE,
            ECMD_CREATE_DATAFORM,
            ECMD_CREATE_ENUM,
            ECMD_INSERT_FUNCTION,
            ECMD_EDIT_FUNCTION,
            ECMD_SET_PRIMARY_KEY,
            ECMD_INSERT_COLUMN,
            ECMD_AUTO_SIZE,
            ECMD_SHOW_RELATION_LABELS,
            VSDGenerateDataSet,
            VSDPreview,
            VSDConfigureAdapter,
            VSDViewDatasetSchema,
            VSDDatasetProperties,
            VSDParameterizeForm,
            VSDAddChildForm,
            ECMD_EDITCONSTRAINT,
            ECMD_DELETECONSTRAINT,
            ECMD_EDITDATARELATION,
            CloseProject = 1982,
            ReloadCommandBars,
            SolutionPlatform = 1990,
            SolutionPlatformGetList,
            ECMD_DATAACCESSOR = 2000,
            ECMD_ADD_DATAACCESSOR,
            ECMD_QUERY,
            ECMD_ADD_QUERY,
            ECMD_PUBLISHSELECTION = 2005,
            ECMD_PUBLISHSLNCTX,
            CallBrowserShowCallsTo = 2010,
            CallBrowserShowCallsFrom,
            CallBrowserShowNewCallsTo,
            CallBrowserShowNewCallsFrom,
            CallBrowser1ShowCallsTo,
            CallBrowser2ShowCallsTo,
            CallBrowser3ShowCallsTo,
            CallBrowser4ShowCallsTo,
            CallBrowser5ShowCallsTo,
            CallBrowser6ShowCallsTo,
            CallBrowser7ShowCallsTo,
            CallBrowser8ShowCallsTo,
            CallBrowser9ShowCallsTo,
            CallBrowser10ShowCallsTo,
            CallBrowser11ShowCallsTo,
            CallBrowser12ShowCallsTo,
            CallBrowser13ShowCallsTo,
            CallBrowser14ShowCallsTo,
            CallBrowser15ShowCallsTo,
            CallBrowser16ShowCallsTo,
            CallBrowser1ShowCallsFrom,
            CallBrowser2ShowCallsFrom,
            CallBrowser3ShowCallsFrom,
            CallBrowser4ShowCallsFrom,
            CallBrowser5ShowCallsFrom,
            CallBrowser6ShowCallsFrom,
            CallBrowser7ShowCallsFrom,
            CallBrowser8ShowCallsFrom,
            CallBrowser9ShowCallsFrom,
            CallBrowser10ShowCallsFrom,
            CallBrowser11ShowCallsFrom,
            CallBrowser12ShowCallsFrom,
            CallBrowser13ShowCallsFrom,
            CallBrowser14ShowCallsFrom,
            CallBrowser15ShowCallsFrom,
            CallBrowser16ShowCallsFrom,
            CallBrowser1ShowFullNames,
            CallBrowser2ShowFullNames,
            CallBrowser3ShowFullNames,
            CallBrowser4ShowFullNames,
            CallBrowser5ShowFullNames,
            CallBrowser6ShowFullNames,
            CallBrowser7ShowFullNames,
            CallBrowser8ShowFullNames,
            CallBrowser9ShowFullNames,
            CallBrowser10ShowFullNames,
            CallBrowser11ShowFullNames,
            CallBrowser12ShowFullNames,
            CallBrowser13ShowFullNames,
            CallBrowser14ShowFullNames,
            CallBrowser15ShowFullNames,
            CallBrowser16ShowFullNames,
            CallBrowser1Settings,
            CallBrowser2Settings,
            CallBrowser3Settings,
            CallBrowser4Settings,
            CallBrowser5Settings,
            CallBrowser6Settings,
            CallBrowser7Settings,
            CallBrowser8Settings,
            CallBrowser9Settings,
            CallBrowser10Settings,
            CallBrowser11Settings,
            CallBrowser12Settings,
            CallBrowser13Settings,
            CallBrowser14Settings,
            CallBrowser15Settings,
            CallBrowser16Settings,
            CallBrowser1SortAlpha,
            CallBrowser2SortAlpha,
            CallBrowser3SortAlpha,
            CallBrowser4SortAlpha,
            CallBrowser5SortAlpha,
            CallBrowser6SortAlpha,
            CallBrowser7SortAlpha,
            CallBrowser8SortAlpha,
            CallBrowser9SortAlpha,
            CallBrowser10SortAlpha,
            CallBrowser11SortAlpha,
            CallBrowser12SortAlpha,
            CallBrowser13SortAlpha,
            CallBrowser14SortAlpha,
            CallBrowser15SortAlpha,
            CallBrowser16SortAlpha,
            CallBrowser1SortAccess,
            CallBrowser2SortAccess,
            CallBrowser3SortAccess,
            CallBrowser4SortAccess,
            CallBrowser5SortAccess,
            CallBrowser6SortAccess,
            CallBrowser7SortAccess,
            CallBrowser8SortAccess,
            CallBrowser9SortAccess,
            CallBrowser10SortAccess,
            CallBrowser11SortAccess,
            CallBrowser12SortAccess,
            CallBrowser13SortAccess,
            CallBrowser14SortAccess,
            CallBrowser15SortAccess,
            CallBrowser16SortAccess,
            ShowCallBrowser = 2120,
            CallBrowser1,
            CallBrowser2,
            CallBrowser3,
            CallBrowser4,
            CallBrowser5,
            CallBrowser6,
            CallBrowser7,
            CallBrowser8,
            CallBrowser9,
            CallBrowser10,
            CallBrowser11,
            CallBrowser12,
            CallBrowser13,
            CallBrowser14,
            CallBrowser15,
            CallBrowser16,
            CallBrowser17,
            GlobalUndo,
            GlobalRedo,
            CallBrowserShowCallsToCmd,
            CallBrowserShowCallsFromCmd,
            CallBrowserShowNewCallsToCmd,
            CallBrowserShowNewCallsFromCmd,
            CallBrowser1Search = 2145,
            CallBrowser2Search,
            CallBrowser3Search,
            CallBrowser4Search,
            CallBrowser5Search,
            CallBrowser6Search,
            CallBrowser7Search,
            CallBrowser8Search,
            CallBrowser9Search,
            CallBrowser10Search,
            CallBrowser11Search,
            CallBrowser12Search,
            CallBrowser13Search,
            CallBrowser14Search,
            CallBrowser15Search,
            CallBrowser16Search,
            CallBrowser1Refresh,
            CallBrowser2Refresh,
            CallBrowser3Refresh,
            CallBrowser4Refresh,
            CallBrowser5Refresh,
            CallBrowser6Refresh,
            CallBrowser7Refresh,
            CallBrowser8Refresh,
            CallBrowser9Refresh,
            CallBrowser10Refresh,
            CallBrowser11Refresh,
            CallBrowser12Refresh,
            CallBrowser13Refresh,
            CallBrowser14Refresh,
            CallBrowser15Refresh,
            CallBrowser16Refresh,
            CallBrowser1SearchCombo = 2180,
            CallBrowser2SearchCombo,
            CallBrowser3SearchCombo,
            CallBrowser4SearchCombo,
            CallBrowser5SearchCombo,
            CallBrowser6SearchCombo,
            CallBrowser7SearchCombo,
            CallBrowser8SearchCombo,
            CallBrowser9SearchCombo,
            CallBrowser10SearchCombo,
            CallBrowser11SearchCombo,
            CallBrowser12SearchCombo,
            CallBrowser13SearchCombo,
            CallBrowser14SearchCombo,
            CallBrowser15SearchCombo,
            CallBrowser16SearchCombo,
            TaskListProviderCombo = 2200,
            TaskListProviderComboList,
            CreateUserTask,
            ErrorListShowErrors = 2210,
            ErrorListShowWarnings,
            ErrorListShowMessages,
            Registration = 2214,
            CallBrowser1SearchComboList,
            CallBrowser2SearchComboList,
            CallBrowser3SearchComboList,
            CallBrowser4SearchComboList,
            CallBrowser5SearchComboList,
            CallBrowser6SearchComboList,
            CallBrowser7SearchComboList,
            CallBrowser8SearchComboList,
            CallBrowser9SearchComboList,
            CallBrowser10SearchComboList,
            CallBrowser11SearchComboList,
            CallBrowser12SearchComboList,
            CallBrowser13SearchComboList,
            CallBrowser14SearchComboList,
            CallBrowser15SearchComboList,
            CallBrowser16SearchComboList,
            SnippetProp = 2240,
            SnippetRef,
            SnippetRepl,
            StartPage = 2245,
            EditorLineFirstColumn = 2250,
            EditorLineFirstColumnExtend,
            SEServerExplorer = 2260,
            SEDataExplorer,
            ViewCallHierarchy = 2301,
            ToggleConsumeFirstCompletionMode = 2303,
            ECMD_VALIDATION_TARGET = 11281,
            ECMD_VALIDATION_TARGET_GET_LIST,
            ECMD_CSS_TARGET,
            ECMD_CSS_TARGET_GET_LIST,
            Design = 12288,
            DesignOn,
            SEDesign = 12291,
            NewDiagram,
            NewTable = 12294,
            NewDBItem = 12302,
            NewTrigger = 12304,
            Debug = 12306,
            NewProcedure,
            NewQuery,
            RefreshLocal,
            DbAddDataConnection = 12311,
            DBDefDBRef,
            RunCmd,
            RunOn,
            NewDBRef,
            SetAsDef,
            CreateCmdFile,
            Cancel,
            NewDatabase = 12320,
            NewUser,
            NewRole,
            ChangeLogin,
            NewView,
            ModifyConnection,
            Disconnect,
            CopyScript,
            AddSCC,
            RemoveSCC,
            GetLatest = 12336,
            CheckOut,
            CheckIn,
            UndoCheckOut,
            AddItemSCC,
            NewPackageSpec,
            NewPackageBody,
            InsertSQL,
            RunSelection,
            UpdateScript,
            NewScript = 12348,
            NewFunction,
            NewTableFunction,
            NewInlineFunction,
            AddDiagram,
            AddTable,
            AddSynonym,
            AddView,
            AddProcedure,
            AddFunction,
            AddTableFunction,
            AddInlineFunction,
            AddPkgSpec,
            AddPkgBody,
            AddTrigger,
            ExportData,
            DbnsVcsAdd,
            DbnsVcsRemove,
            DbnsVcsCheckout,
            DbnsVcsUndoCheckout,
            DbnsVcsCheckin,
            SERetrieveData = 12384,
            SEEditTextObject,
            DesignSQLBlock = 12388,
            RegisterSQLInstance,
            UnregisterSQLInstance,
            CommandWindowSaveScript = 12550,
            CommandWindowRunScript,
            CommandWindowCursorUp,
            CommandWindowCursorDown,
            CommandWindowCursorLeft,
            CommandWindowCursorRight,
            CommandWindowHistoryUp,
            CommandWindowHistoryDown
        }

        [Guid("5DD0BB59-7076-4C59-88D3-DE36931F63F0")]
        public enum VSStd2010CmdID
        {
            DynamicToolBarListFirst = 1,
            DynamicToolBarListLast = 300,
            WindowFrameDockMenu = 500,
            NextDocumentTab = 600,
            PreviousDocumentTab,
            ShellNavigate1First = 1000,
            ShellNavigate2First = 1033,
            ShellNavigate3First = 1066,
            ShellNavigate4First = 1099,
            ShellNavigate5First = 1132,
            ShellNavigate6First = 1165,
            ShellNavigate7First = 1198,
            ShellNavigate8First = 1231,
            ShellNavigate9First = 1264,
            ShellNavigate10First = 1297,
            ShellNavigate11First = 1330,
            ShellNavigate12First = 1363,
            ShellNavigate13First = 1396,
            ShellNavigate14First = 1429,
            ShellNavigate15First = 1462,
            ShellNavigate16First = 1495,
            ShellNavigate17First = 1528,
            ShellNavigate18First = 1561,
            ShellNavigate19First = 1594,
            ShellNavigate20First = 1627,
            ShellNavigate21First = 1660,
            ShellNavigate22First = 1693,
            ShellNavigate23First = 1726,
            ShellNavigate24First = 1759,
            ShellNavigate25First = 1792,
            ShellNavigate26First = 1825,
            ShellNavigate27First = 1858,
            ShellNavigate28First = 1891,
            ShellNavigate29First = 1924,
            ShellNavigate30First = 1957,
            ShellNavigate31First = 1990,
            ShellNavigate32First = 2023,
            ShellNavigateLast = 2055,
            ZoomIn = 2100,
            ZoomOut,
            OUTLN_EXPAND_ALL = 2500,
            OUTLN_COLLAPSE_ALL,
            OUTLN_EXPAND_CURRENT,
            OUTLN_COLLAPSE_CURRENT,
            ExtensionManager = 3000
        }

        [Guid("D63DB1F0-404E-4B21-9648-CA8D99245EC3")]
        public enum VSStd11CmdID
        {
            FloatAll = 1,
            MoveAllToNext,
            MoveAllToPrevious,
            MultiSelect,
            PaneNextTabAndMultiSelect,
            PanePrevTabAndMultiSelect,
            PinTab,
            BringFloatingWindowsToFront,
            PromoteTab,
            MoveToMainTabWell,
            ToggleFilter,
            FilterToCurrentProject,
            FilterToCurrentDocument,
            FilterToOpenDocuments,
            WindowSearch = 17,
            GlobalSearch,
            GlobalSearchBack,
            SolutionExplorerSearch,
            StartupProjectProperties,
            CloseAllButPinned,
            ResolveFaultedProjects,
            ExecuteSelectionInInteractive,
            ExecuteLineInInteractive,
            InteractiveSessionInterrupt,
            InteractiveSessionRestart,
            SolutionExplorerCollapseAll = 29,
            SolutionExplorerBack,
            SolutionExplorerHome,
            SolutionExplorerForward = 33,
            SolutionExplorerNewScopedWindow,
            SolutionExplorerToggleSingleClickPreview,
            SolutionExplorerSyncWithActiveDocument,
            NewProjectFromTemplate,
            SolutionExplorerScopeToThis,
            SolutionExplorerFilterOpened,
            SolutionExplorerFilterPendingChanges,
            PasteAsLink,
            LocateFindTarget
        }

        [Guid("2A8866DC-7BDE-4dc8-A360-A60679534384")]
        public enum VSStd12CmdID
        {
            ShowUserNotificationsToolWindow = 1,
            OpenProjectFromScc,
            ShareProject,
            PeekDefinition,
            AccountSettings,
            PeekNavigateForward,
            PeekNavigateBackward,
            RetargetProject,
            RetargetProjectInstallComponent,
            AddReferenceProjectOnly,
            AddWebReferenceProjectOnly,
            AddServiceReferenceProjectOnly,
            AddReferenceNonProjectOnly,
            AddWebReferenceNonProjectOnly,
            AddServiceReferenceNonProjectOnly,
            NavigateTo = 256,
            MoveSelLinesUp = 258,
            MoveSelLinesDown
        }

        [Guid("4C7763BF-5FAF-4264-A366-B7E1F27BA958")]
        public enum VSStd14CmdID
        {
            ShowQuickFixes = 1,
            ShowRefactorings,
            SmartBreakLine,
            ManageWindowLayouts,
            SaveWindowLayout,
            ShowQuickFixesForPosition,
            DeleteFR1 = 10,
            DeleteFR2 = 20,
            ErrorContextComboList = 30,
            ErrorContextComboGetList,
            ErrorBuildContextComboList = 40,
            ErrorBuildContextComboGetList,
            ErrorListClearFilters = 50,
            WindowLayoutList0 = 4096,
            WindowLayoutList1,
            WindowLayoutList2,
            WindowLayoutList3,
            WindowLayoutList4,
            WindowLayoutList5,
            WindowLayoutList6,
            WindowLayoutList7,
            WindowLayoutList8,
            WindowLayoutList9,
            WindowLayoutListFirst = 4096,
            WindowLayoutListDynamicFirst = 4112,
            WindowLayoutListLast = 8191
        }

        [Flags]
        public enum CEF : uint
        {
            CloneFile = 1u,
            OpenFile = 2u,
            Silent = 4u,
            OpenAsNew = 8u
        }

        [Guid("60481700-078b-11d1-aaf8-00a0c9055a90")]
        public enum VsUIHierarchyWindowCmdIds
        {
            UIHWCMDID_RightClick = 1,
            UIHWCMDID_DoubleClick,
            UIHWCMDID_EnterKey,
            UIHWCMDID_StartLabelEdit,
            UIHWCMDID_CommitLabelEdit,
            UIHWCMDID_CancelLabelEdit
        }

        public enum VSSELELEMID
        {
            SEID_UndoManager,
            SEID_WindowFrame,
            SEID_DocumentFrame,
            SEID_StartupProject,
            SEID_PropertyBrowserSID,
            SEID_UserContext,
            SEID_ResultList,
            SEID_LastWindowFrame
        }

        public static class VsPackageGuid
        {
            public const string VsEnvironmentPackage_string = "{DA9FB551-C724-11D0-AE1F-00A0C90FFFC3}";

            public static readonly Guid VsEnvironmentPackage_guid = new Guid("{DA9FB551-C724-11D0-AE1F-00A0C90FFFC3}");

            public const string HtmlEditorPackage_string = "{1B437D20-F8FE-11D2-A6AE-00104BCC7269}";

            public static readonly Guid HtmlEditorPackage_guid = new Guid("{1B437D20-F8FE-11D2-A6AE-00104BCC7269}");

            public const string VsTaskListPackage_string = "{4A9B7E50-AA16-11D0-A8C5-00A0C921A4D2}";

            public static readonly Guid VsTaskListPackage_guid = new Guid("{4A9B7E50-AA16-11D0-A8C5-00A0C921A4D2}");

            public const string VsDocOutlinePackage_string = "{21AF45B0-FFA5-11D0-B63F-00A0C922E851}";

            public static readonly Guid VsDocOutlinePackage_guid = new Guid("{21AF45B0-FFA5-11D0-B63F-00A0C922E851}");
        }

        public static class VsEditorFactoryGuid
        {
            public const string HtmlEditor_string = "{C76D83F8-A489-11D0-8195-00A0C91BBEE3}";

            public static readonly Guid HtmlEditor_guid = new Guid("{C76D83F8-A489-11D0-8195-00A0C91BBEE3}");

            public const string TextEditor_string = "{8B382828-6202-11d1-8870-0000F87579D2}";

            public static readonly Guid TextEditor_guid = new Guid("{8B382828-6202-11d1-8870-0000F87579D2}");

            public const string ExternalEditor_string = "{8B382828-6202-11D1-8870-0000F87579D2}";

            public static readonly Guid ExternalEditor_guid = new Guid("{8B382828-6202-11D1-8870-0000F87579D2}");

            public const string ProjectDesignerEditor_string = "{04B8AB82-A572-4FEF-95CE-5222444B6B64}";

            public static readonly Guid ProjectDesignerEditor_guid = new Guid("{04B8AB82-A572-4FEF-95CE-5222444B6B64}");
        }

        public static class VsLanguageServiceGuid
        {
            public const string HtmlLanguageService_string = "{58E975A0-F8FE-11D2-A6AE-00104BCC7269}";

            public static readonly Guid HtmlLanguageService_guid = new Guid("{58E975A0-F8FE-11D2-A6AE-00104BCC7269}");
        }

        public static class OutputWindowPaneGuid
        {
            public const string BuildOutputPane_string = "{1BD8A850-02D1-11D1-BEE7-00A0C913D1F8}";

            public static readonly Guid BuildOutputPane_guid = new Guid("{1BD8A850-02D1-11D1-BEE7-00A0C913D1F8}");

            public const string SortedBuildOutputPane_string = "{2032B126-7C8D-48AD-8026-0E0348004FC0}";

            public static readonly Guid SortedBuildOutputPane_guid = new Guid("{2032B126-7C8D-48AD-8026-0E0348004FC0}");

            public const string DebugPane_string = "{FC076020-078A-11D1-A7DF-00A0C9110051}";

            public static readonly Guid DebugPane_guid = new Guid("{FC076020-078A-11D1-A7DF-00A0C9110051}");

            public const string GeneralPane_string = "{3C24D581-5591-4884-A571-9FE89915CD64}";

            public static readonly Guid GeneralPane_guid = new Guid("{3C24D581-5591-4884-A571-9FE89915CD64}");

            public const string StoreValidationPane_string = "{54065C74-1B11-4249-9EA7-5540D1A6D528}";

            public static readonly Guid StoreValidationPane_guid = new Guid("{54065C74-1B11-4249-9EA7-5540D1A6D528}");
        }

        public static class ItemTypeGuid
        {
            public const string PhysicalFile_string = "{6BB5F8EE-4483-11D3-8BCF-00C04F8EC28C}";

            public static readonly Guid PhysicalFile_guid = new Guid("{6BB5F8EE-4483-11D3-8BCF-00C04F8EC28C}");

            public const string PhysicalFolder_string = "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}";

            public static readonly Guid PhysicalFolder_guid = new Guid("{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}");

            public const string VirtualFolder_string = "{6BB5F8F0-4483-11D3-8BCF-00C04F8EC28C}";

            public static readonly Guid VirtualFolder_guid = new Guid("{6BB5F8F0-4483-11D3-8BCF-00C04F8EC28C}");

            public const string SubProject_string = "{EA6618E8-6E24-4528-94BE-6889FE16485C}";

            public static readonly Guid SubProject_guid = new Guid("{EA6618E8-6E24-4528-94BE-6889FE16485C}");

            public const string SharedProjectReference_string = "{FBA6BD9A-47F3-4C04-BDC0-7F76A9E2E582}";

            public static readonly Guid SharedProjectReference_guid = new Guid("{FBA6BD9A-47F3-4C04-BDC0-7F76A9E2E582}");
        }

        public static class CodeModelLanguage
        {
            public const string VC = "{B5E9BD32-6D3E-4B5D-925E-8A43B79820B4}";

            public const string VB = "{B5E9BD33-6D3E-4B5D-925E-8A43B79820B4}";

            public const string CSharp = "{B5E9BD34-6D3E-4B5D-925E-8A43B79820B4}";

            public const string IDL = "{B5E9BD35-6D3E-4B5D-925E-8A43B79820B4}";

            public const string MC = "{B5E9BD36-6D3E-4B5D-925E-8A43B79820B4}";
        }

        public static class WizardType
        {
            public const string AddSubProject = "{0F90E1D2-4999-11D1-B6D1-00A0C90F2744}";

            public const string AddItem = "{0F90E1D1-4999-11D1-B6D1-00A0C90F2744}";

            public const string NewProject = "{0F90E1D0-4999-11D1-B6D1-00A0C90F2744}";
        }

        public static class VsDependencyTypeGuid
        {
            public const string BuildProject_string = "{707D11B6-91CA-11D0-8A3E-00A0C91E2ACD}";

            public static readonly Guid BuildProject_guid = new Guid("{707D11B6-91CA-11D0-8A3E-00A0C91E2ACD}");
        }

        public static class VsLanguageUserDataGuid
        {
            public const string SupportCF_HTML_string = "{27E97702-589E-11D2-8233-0080C747D9A0}";

            public static readonly Guid SupportCF_HTML_guid = new Guid("{27E97702-589E-11D2-8233-0080C747D9A0}");
        }

        public static class VsTextBufferUserDataGuid
        {
            public const string VsBufferMoniker_string = "{978A8E17-4DF8-432A-9623-D530A26452BC}";

            public static readonly Guid VsBufferMoniker_guid = new Guid("{978A8E17-4DF8-432A-9623-D530A26452BC}");

            public const string VsBufferIsDiskFile_string = "{D9126592-1473-11D3-BEC6-0080C747D9A0}";

            public static readonly Guid VsBufferIsDiskFile_guid = new Guid("{D9126592-1473-11D3-BEC6-0080C747D9A0}");

            public const string VsBufferEncodingVSTFF_string = "{16417F39-A6B7-4C90-89FA-770D2C60440B}";

            public static readonly Guid VsBufferEncodingVSTFF_guid = new Guid("{16417F39-A6B7-4C90-89FA-770D2C60440B}");

            public const string VsBufferEncodingPromptOnLoad_string = "{99EC03F0-C843-4C09-BE74-CDCA5158D36C}";

            public static readonly Guid VsBufferEncodingPromptOnLoad_guid = new Guid("{99EC03F0-C843-4C09-BE74-CDCA5158D36C}");

            public const string VsBufferDetectCharSet_string = "{36358D1F-BF7E-11D1-B03A-00C04FB68006}";

            public static readonly Guid VsBufferDetectCharSet_guid = new Guid("{36358D1F-BF7E-11D1-B03A-00C04FB68006}");

            public const string VsBufferDetectLangSID_string = "{17F375AC-C814-11D1-88AD-0000F87579D2}";

            public static readonly Guid VsBufferDetectLangSID_guid = new Guid("{17F375AC-C814-11D1-88AD-0000F87579D2}");

            public const string PropertyBrowserSID_string = "{CE6DDBBA-8D13-11D1-8889-0000F87579D2}";

            public static readonly Guid PropertyBrowserSID_guid = new Guid("{CE6DDBBA-8D13-11D1-8889-0000F87579D2}");

            public const string UserReadOnlyErrorString_string = "{A3BCFE56-CF1B-11D1-88B1-0000F87579D2}";

            public static readonly Guid UserReadOnlyErrorString_guid = new Guid("{A3BCFE56-CF1B-11D1-88B1-0000F87579D2}");

            public const string BufferStorage_string = "{D97F167A-638E-11D2-88F6-0000F87579D2}";

            public static readonly Guid BufferStorage_guid = new Guid("{D97F167A-638E-11D2-88F6-0000F87579D2}");

            public const string VsBufferExtraFiles_string = "{FD494BF6-1167-4635-A20C-5C24B2D7B33D}";

            public static readonly Guid VsBufferExtraFiles_guid = new Guid("{FD494BF6-1167-4635-A20C-5C24B2D7B33D}");

            public const string VsBufferFileReload_string = "{80D2B881-81A3-4F0B-BCF0-70A0054E672F}";

            public static readonly Guid VsBufferFileReload_guid = new Guid("{80D2B881-81A3-4F0B-BCF0-70A0054E672F}");

            public const string VsInitEncodingDialogFromUserData_string = "{C2382D84-6650-4386-860F-248ECB222FC1}";

            public static readonly Guid VsInitEncodingDialogFromUserData_guid = new Guid("{C2382D84-6650-4386-860F-248ECB222FC1}");

            public const string VsBufferContentType_string = "{1BEB4195-98F4-4589-80E0-480CE32FF059}";

            public static readonly Guid VsBufferContentType_guid = new Guid("{1BEB4195-98F4-4589-80E0-480CE32FF059}");

            public const string VsTextViewRoles_string = "{297078FF-81A2-43D8-9CA3-4489C53C99BA}";

            public static readonly Guid VsTextViewRoles_guid = new Guid("{297078FF-81A2-43D8-9CA3-4489C53C99BA}");
        }

        public static class EditPropyCategoryGuid
        {
            public const string TextManagerGlobal_string = "{6BFB60A2-48D8-424E-81A2-040ACA0B1F68}";

            public static readonly Guid TextManagerGlobal_guid = new Guid("{6BFB60A2-48D8-424E-81A2-040ACA0B1F68}");

            public const string ViewMasterSettings_string = "{D1756E7C-B7FD-49A8-B48E-87B14A55655A}";

            public static readonly Guid ViewMasterSettings_guid = new Guid("{D1756E7C-B7FD-49A8-B48E-87B14A55655A}");
        }

        public static class CATID
        {
            public const string CSharpFileProperties_string = "{8D58E6AF-ED4E-48B0-8C7B-C74EF0735451}";

            public static readonly Guid CSharpFileProperties_guid = new Guid("{8D58E6AF-ED4E-48B0-8C7B-C74EF0735451}");

            public const string CSharpFolderProperties_string = "{914FE278-054A-45DB-BF9E-5F22484CC84C}";

            public static readonly Guid CSharpFolderProperties_guid = new Guid("{914FE278-054A-45DB-BF9E-5F22484CC84C}");

            public const string ProjectAutomationObject_string = "{610D4614-D0D5-11D2-8599-006097C68E81}";

            public static readonly Guid ProjectAutomationObject_guid = new Guid("{610D4614-D0D5-11D2-8599-006097C68E81}");

            public const string ProjectItemAutomationObject_string = "{610D4615-D0D5-11D2-8599-006097C68E81}";

            public static readonly Guid ProjectItemAutomationObject_guid = new Guid("{610D4615-D0D5-11D2-8599-006097C68E81}");

            public const string VBAFileProperties_string = "{AC2912B2-50ED-4E62-8DFF-429B4B88FC9E}";

            public static readonly Guid VBAFileProperties_guid = new Guid("{AC2912B2-50ED-4E62-8DFF-429B4B88FC9E}");

            public const string VBAFolderProperties_string = "{79231B36-6213-481D-AA7D-0F931E8F2CF9}";

            public static readonly Guid VBAFolderProperties_guid = new Guid("{79231B36-6213-481D-AA7D-0F931E8F2CF9}");

            public const string VBFileProperties_string = "{EA5BD05D-3C72-40A5-95A0-28A2773311CA}";

            public static readonly Guid VBFileProperties_guid = new Guid("{EA5BD05D-3C72-40A5-95A0-28A2773311CA}");

            public const string VBFolderProperties_string = "{932DC619-2EAA-4192-B7E6-3D15AD31DF49}";

            public static readonly Guid VBFolderProperties_guid = new Guid("{932DC619-2EAA-4192-B7E6-3D15AD31DF49}");

            public const string VBProjectProperties_string = "{E0FDC879-C32A-4751-A3D3-0B3824BD575F}";

            public static readonly Guid VBProjectProperties_guid = new Guid("{E0FDC879-C32A-4751-A3D3-0B3824BD575F}");

            public const string VBReferenceProperties_string = "{2289B812-8191-4E81-B7B3-174045AB0CB5}";

            public static readonly Guid VBReferenceProperties_guid = new Guid("{2289B812-8191-4E81-B7B3-174045AB0CB5}");

            public const string VCProjectNode_string = "{EE8299CB-19B6-4F20-ABEA-E1FD9A33B683}";

            public static readonly Guid VCProjectNode_guid = new Guid("{EE8299CB-19B6-4F20-ABEA-E1FD9A33B683}");

            public const string VCFileGroup_string = "{EE8299CA-19B6-4F20-ABEA-E1FD9A33B683}";

            public static readonly Guid VCFileGroup_guid = new Guid("{EE8299CA-19B6-4F20-ABEA-E1FD9A33B683}");

            public const string VCFileNode_string = "{EE8299C9-19B6-4F20-ABEA-E1FD9A33B683}";

            public static readonly Guid VCFileNode_guid = new Guid("{EE8299C9-19B6-4F20-ABEA-E1FD9A33B683}");

            public const string VCAssemblyReferenceNode_string = "{FE8299C9-19B6-4F20-ABEA-E1FD9A33B683}";

            public static readonly Guid VCAssemblyReferenceNode_guid = new Guid("{FE8299C9-19B6-4F20-ABEA-E1FD9A33B683}");

            public const string VCProjectReferenceNode_string = "{593DCFCE-20A7-48E4-ACA1-49ADE9049887}";

            public static readonly Guid VCProjectReferenceNode_guid = new Guid("{593DCFCE-20A7-48E4-ACA1-49ADE9049887}");

            public const string VCActiveXReferenceNode_string = "{9E8182D3-C60A-44F4-A74B-14C90EF9CACE}";

            public static readonly Guid VCActiveXReferenceNode_guid = new Guid("{9E8182D3-C60A-44F4-A74B-14C90EF9CACE}");

            public const string VCReferences_string = "{FE8299CA-19B6-4F20-ABEA-E1FD9A33B683}";

            public static readonly Guid VCReferences_guid = new Guid("{FE8299CA-19B6-4F20-ABEA-E1FD9A33B683}");
        }

        public static class CLSID
        {
            public const string MiscellaneousFilesProject_string = "{A2FE74E1-B743-11D0-AE1A-00A0C90FFFC3}";

            public static readonly Guid MiscellaneousFilesProject_guid = new Guid("{A2FE74E1-B743-11D0-AE1A-00A0C90FFFC3}");

            public const string SolutionFolderProject_string = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}";

            public static readonly Guid SolutionFolderProject_guid = new Guid("{2150E333-8FDC-42A3-9474-1A3956D46DE8}");

            public const string SolutionItemsProject_string = "{D1DCDB85-C5E8-11D2-BFCA-00C04F990235}";

            public static readonly Guid SolutionItemsProject_guid = new Guid("{D1DCDB85-C5E8-11D2-BFCA-00C04F990235}");

            public const string VsTextBuffer_string = "{8E7B96A8-E33D-11d0-A6D5-00C04FB67F6A}";

            public static readonly Guid VsTextBuffer_guid = new Guid("{8E7B96A8-E33D-11d0-A6D5-00C04FB67F6A}");

            public const string UnloadedProject_string = "{76E22BD3-C2EC-47F1-802B-53197756DAE8}";

            public static readonly Guid UnloadedProject_guid = new Guid("{76E22BD3-C2EC-47F1-802B-53197756DAE8}");

            public const string VsCfgProviderEventsHelper_string = "{99913F1F-1EE3-11D1-8A6E-00C04F682E21}";

            public static readonly Guid VsCfgProviderEventsHelper_guid = new Guid("{99913F1F-1EE3-11D1-8A6E-00C04F682E21}");

            public const string VsEnvironmentPackage_string = "{DA9FB551-C724-11D0-AE1F-00A0C90FFFC3}";

            public static readonly Guid VsEnvironmentPackage_guid = new Guid("{DA9FB551-C724-11D0-AE1F-00A0C90FFFC3}");

            public const string VsTaskListPackage_string = "{4A9B7E50-AA16-11D0-A8C5-00A0C921A4D2}";

            public static readonly Guid VsTaskListPackage_guid = new Guid("{4A9B7E50-AA16-11D0-A8C5-00A0C921A4D2}");

            public const string VsUIWpfLoader_string = "{0B127700-143C-4AB5-9D39-BFF47151B563}";

            public static readonly Guid VsUIWpfLoader_guid = new Guid("{0B127700-143C-4AB5-9D39-BFF47151B563}");

            public const string VsSearchQueryParser_string = "{B71B3DF9-7A4A-4D70-8293-3874DB098FDD}";

            public static readonly Guid VsSearchQueryParser_guid = new Guid("{B71B3DF9-7A4A-4D70-8293-3874DB098FDD}");

            public const string HtmDocData_string = "{62C81794-A9EC-11D0-8198-00A0C91BBEE3}";

            public static readonly Guid HtmDocData_guid = new Guid("{62C81794-A9EC-11D0-8198-00A0C91BBEE3}");

            public const string VsUIHierarchyWindow_string = "{7D960B07-7AF8-11D0-8E5E-00A0C911005A}";

            public static readonly Guid VsUIHierarchyWindow_guid = new Guid("{7D960B07-7AF8-11D0-8E5E-00A0C911005A}");

            public const string VsTaskList_string = "{BC5955D5-AA0D-11D0-A8C5-00A0C921A4D2}";

            public static readonly Guid VsTaskList_guid = new Guid("{BC5955D5-AA0D-11D0-A8C5-00A0C921A4D2}");
        }

        public static class DebugEnginesGuids
        {
            [EditorBrowsable(EditorBrowsableState.Never)]
            public static readonly Guid NativeOnly = new Guid("{3B476D35-A401-11D2-AAD4-00C04F990171}");

            [EditorBrowsable(EditorBrowsableState.Never)]
            public static readonly Guid Script = new Guid("{F200A7E7-DEA5-11D0-B854-00A0244A1DE2}");

            [EditorBrowsable(EditorBrowsableState.Never)]
            public static readonly Guid ManagedAndNative = new Guid("{92EF0900-2251-11D2-B72E-0000F87572EF}");

            [EditorBrowsable(EditorBrowsableState.Never)]
            public static readonly Guid SQLLocalEngine = new Guid("{E04BDE58-45EC-48DB-9807-513F78865212}");

            [EditorBrowsable(EditorBrowsableState.Never)]
            public static readonly Guid SqlDebugEngine2 = new Guid("{3B476D30-A401-11D2-AAD4-00C04F990171}");

            [EditorBrowsable(EditorBrowsableState.Never)]
            public static readonly Guid SqlDebugEngine3 = new Guid("{3B476D3A-A401-11D2-AAD4-00C04F990171}");

            public const string ManagedOnly_string = "{449EC4CC-30D2-4032-9256-EE18EB41B62B}";

            public static readonly Guid ManagedOnly_guid = new Guid("{449EC4CC-30D2-4032-9256-EE18EB41B62B}");

            public const string ManagedOnlyEngineV2_string = "{5FFF7536-0C87-462D-8FD2-7971D948E6DC}";

            public static readonly Guid ManagedOnlyEngineV2_guid = new Guid("{5FFF7536-0C87-462D-8FD2-7971D948E6DC}");

            public const string ManagedOnlyEngineV4_string = "{FB0D4648-F776-4980-95F8-BB7F36EBC1EE}";

            public static readonly Guid ManagedOnlyEngineV4_guid = new Guid("{FB0D4648-F776-4980-95F8-BB7F36EBC1EE}");

            public const string CoreSystemClr_string = "{2E36F1D4-B23C-435D-AB41-18E608940038}";

            public static readonly Guid CoreSystemClr_guid = new Guid("{2E36F1D4-B23C-435D-AB41-18E608940038}");

            public const string COMPlusLegacyEngine_string = "{351668CC-8477-4fbf-BFE3-5F1006E4DB1F}";

            public static readonly Guid COMPlusLegacyEngine_guid = new Guid("{351668CC-8477-4fbf-BFE3-5F1006E4DB1F}");

            public const string COMPlusNewArchEngine_string = "{97552AEF-4F41-447a-BCC3-802EAA377343}";

            public static readonly Guid COMPlusNewArchEngine_guid = new Guid("{97552AEF-4F41-447a-BCC3-802EAA377343}");

            public const string NativeOnly_string = "{3B476D35-A401-11D2-AAD4-00C04F990171}";

            public static readonly Guid NativeOnly_guid = new Guid("{3B476D35-A401-11D2-AAD4-00C04F990171}");

            public const string Script_string = "{F200A7E7-DEA5-11D0-B854-00A0244A1DE2}";

            public static readonly Guid Script_guid = new Guid("{F200A7E7-DEA5-11D0-B854-00A0244A1DE2}");

            public const string ManagedAndNative_string = "{92EF0900-2251-11D2-B72E-0000F87572EF}";

            public static readonly Guid ManagedAndNative_guid = new Guid("{92EF0900-2251-11D2-B72E-0000F87572EF}");

            public const string SQLLocalEngine_string = "{E04BDE58-45EC-48DB-9807-513F78865212}";

            public static readonly Guid SQLLocalEngine_guid = new Guid("{E04BDE58-45EC-48DB-9807-513F78865212}");

            public const string SqlDebugEngine2_string = "{3B476D30-A401-11D2-AAD4-00C04F990171}";

            public static readonly Guid SqlDebugEngine2_guid = new Guid("{3B476D30-A401-11D2-AAD4-00C04F990171}");

            public const string SqlDebugEngine3_string = "{3B476D3A-A401-11D2-AAD4-00C04F990171}";

            public static readonly Guid SqlDebugEngine3_guid = new Guid("{3B476D3A-A401-11D2-AAD4-00C04F990171}");
        }

        public static class DebugPortSupplierGuids
        {
            public const string NoAuth_string = "{3b476d38-a401-11d2-aad4-00c04f990171}";

            public static readonly Guid NoAuth_guid = new Guid("{3b476d38-a401-11d2-aad4-00c04f990171}");
        }

        public static class ProjectTargets
        {
            public static readonly Guid AppContainer_Win8 = new Guid("676F25AF-340F-41F0-986E-6BDD00C6DD63");

            public static readonly Guid AppContainer_Win8_1 = new Guid("239B20B0-620F-4789-B69A-D1C2223AAE11");

            public static readonly Guid WindowsPhone_80SL = new Guid("DE9F6B31-C1E5-B965-95F3-1885AF956FC9");

            public static readonly Guid WindowsPhone_81SL = new Guid("FAC6B224-1737-05C3-1859-1DC6BF8C3D9E");

            public static readonly Guid OneCore_1 = new Guid("08172433-85AA-424E-A9F1-C140350D8FB5");
        }

        public static class SetupDrivers
        {
            public static readonly Guid SetupDriver_VS = new Guid("E2AB05D1-8DAF-4D53-8EFA-CFF4383695C1");

            public static readonly Guid SetupDriver_WebPI = new Guid("114414D4-597D-4C7B-8421-9B49C54E5302");

            public static readonly Guid SetupDriver_OOBFeed = new Guid("3F40C28E-C61D-4137-9C22-C0DD553C6344");
        }

        public static class UICONTEXT
        {
            public const string RESXEditor_string = "{FEA4DCC9-3645-44CD-92E7-84B55A16465C}";

            public static readonly Guid RESXEditor_guid = new Guid("{FEA4DCC9-3645-44CD-92E7-84B55A16465C}");

            public const string SettingsDesigner_string = "{515231AD-C9DC-4AA3-808F-E1B65E72081C}";

            public static readonly Guid SettingsDesigner_guid = new Guid("{515231AD-C9DC-4AA3-808F-E1B65E72081C}");

            public const string PropertyPageDesigner_string = "{86670EFA-3C28-4115-8776-A4D5BB1F27CC}";

            public static readonly Guid PropertyPageDesigner_guid = new Guid("{86670EFA-3C28-4115-8776-A4D5BB1F27CC}");

            public const string ApplicationDesigner_string = "{D06CD5E3-D961-44DC-9D80-C89A1A8D9D56}";

            public static readonly Guid ApplicationDesigner_guid = new Guid("{D06CD5E3-D961-44DC-9D80-C89A1A8D9D56}");

            public const string VBProjOpened_string = "{9DA22B82-6211-11d2-9561-00600818403B}";

            public static readonly Guid VBProjOpened_guid = new Guid("{9DA22B82-6211-11d2-9561-00600818403B}");

            public const string CodeWindow_string = "{8FE2DF1D-E0DA-4EBE-9D5C-415D40E487B5}";

            public static readonly Guid CodeWindow_guid = new Guid("{8FE2DF1D-E0DA-4EBE-9D5C-415D40E487B5}");

            public const string DataSourceWindowAutoVisible_string = "{2E78870D-AC7C-4460-A4A1-3FE37D00EF81}";

            public static readonly Guid DataSourceWindowAutoVisible_guid = new Guid("{2E78870D-AC7C-4460-A4A1-3FE37D00EF81}");

            public const string DataSourceWizardSuppressed_string = "{5705AD15-40EE-4426-AD3E-BA750610D599}";

            public static readonly Guid DataSourceWizardSuppressed_guid = new Guid("{5705AD15-40EE-4426-AD3E-BA750610D599}");

            public const string DataSourceWindowSupported_string = "{95C314C4-660B-4627-9F82-1BAF1C764BBF}";

            public static readonly Guid DataSourceWindowSupported_guid = new Guid("{95C314C4-660B-4627-9F82-1BAF1C764BBF}");

            public const string Debugging_string = "{ADFC4E61-0397-11D1-9F4E-00A0C911004F}";

            public static readonly Guid Debugging_guid = new Guid("{ADFC4E61-0397-11D1-9F4E-00A0C911004F}");

            public const string DesignMode_string = "{ADFC4E63-0397-11D1-9F4E-00A0C911004F}";

            public static readonly Guid DesignMode_guid = new Guid("{ADFC4E63-0397-11D1-9F4E-00A0C911004F}");

            public const string Dragging_string = "{B706F393-2E5B-49E7-9E2E-B1825F639B63}";

            public static readonly Guid Dragging_guid = new Guid("{B706F393-2E5B-49E7-9E2E-B1825F639B63}");

            public const string EmptySolution_string = "{ADFC4E65-0397-11D1-9F4E-00A0C911004F}";

            public static readonly Guid EmptySolution_guid = new Guid("{ADFC4E65-0397-11D1-9F4E-00A0C911004F}");

            public const string FirstLaunchSetup_string = "{E7B2B2DB-973B-4CE9-A8D7-8498895DEA73}";

            public static readonly Guid FirstLaunchSetup_guid = new Guid("{E7B2B2DB-973B-4CE9-A8D7-8498895DEA73}");

            public const string FullScreenMode_string = "{ADFC4E62-0397-11D1-9F4E-00A0C911004F}";

            public static readonly Guid FullScreenMode_guid = new Guid("{ADFC4E62-0397-11D1-9F4E-00A0C911004F}");

            public const string HistoricalDebugging_string = "{D1B1E38F-1A7E-4236-AF55-6FA8F5FA76E6}";

            public static readonly Guid HistoricalDebugging_guid = new Guid("{D1B1E38F-1A7E-4236-AF55-6FA8F5FA76E6}");

            public const string NoSolution_string = "{ADFC4E64-0397-11D1-9F4E-00A0C911004F}";

            public static readonly Guid NoSolution_guid = new Guid("{ADFC4E64-0397-11D1-9F4E-00A0C911004F}");

            public const string NotBuildingAndNotDebugging_string = "{48EA4A80-F14E-4107-88FA-8D0016F30B9C}";

            public static readonly Guid NotBuildingAndNotDebugging_guid = new Guid("{48EA4A80-F14E-4107-88FA-8D0016F30B9C}");

            public const string OsWindows8OrHigher_string = "{67CFF80C-0863-4202-A4E4-CE80FDF8506E}";

            public static readonly Guid OsWindows8OrHigher_guid = new Guid("{67CFF80C-0863-4202-A4E4-CE80FDF8506E}");

            public const string ToolboxVisible_string = "{643905EE-DAE9-4F52-A343-6A5A7349D52C}";

            public static readonly Guid ToolboxVisible_guid = new Guid("{643905EE-DAE9-4F52-A343-6A5A7349D52C}");

            public const string ProjectRetargeting_string = "{DE039A0E-C18F-490C-944A-888B8E86DA4B}";

            public static readonly Guid ProjectRetargeting_guid = new Guid("{DE039A0E-C18F-490C-944A-888B8E86DA4B}");

            public const string SolutionBuilding_string = "{ADFC4E60-0397-11D1-9F4E-00A0C911004F}";

            public static readonly Guid SolutionBuilding_guid = new Guid("{ADFC4E60-0397-11D1-9F4E-00A0C911004F}");

            public const string SolutionExists_string = "{F1536EF8-92EC-443C-9ED7-FDADF150DA82}";

            public static readonly Guid SolutionExists_guid = new Guid("{F1536EF8-92EC-443C-9ED7-FDADF150DA82}");

            public const string SolutionExistsAndFullyLoaded_string = "{10534154-102D-46E2-ABA8-A6BFA25BA0BE}";

            public static readonly Guid SolutionExistsAndFullyLoaded_guid = new Guid("{10534154-102D-46E2-ABA8-A6BFA25BA0BE}");

            public const string SolutionExistsAndNotBuildingAndNotDebugging_string = "{D0E4DEEC-1B53-4CDA-8559-D454583AD23B}";

            public static readonly Guid SolutionExistsAndNotBuildingAndNotDebugging_guid = new Guid("{D0E4DEEC-1B53-4CDA-8559-D454583AD23B}");

            public const string SolutionHasMultipleProjects_string = "{93694FA0-0397-11D1-9F4E-00A0C911004F}";

            public static readonly Guid SolutionHasMultipleProjects_guid = new Guid("{93694FA0-0397-11D1-9F4E-00A0C911004F}");

            public const string SolutionHasSingleProject_string = "{ADFC4E66-0397-11D1-9F4E-00A0C911004F}";

            public static readonly Guid SolutionHasSingleProject_guid = new Guid("{ADFC4E66-0397-11D1-9F4E-00A0C911004F}");

            public const string SolutionHasAppContainerProject_string = "{7CAC4AE1-2E6B-4B02-A91C-71611E86F273}";

            public static readonly Guid SolutionHasAppContainerProject_guid = new Guid("{7CAC4AE1-2E6B-4B02-A91C-71611E86F273}");

            public const string SolutionOpening_string = "{D2567162-F94F-4091-8798-A096E61B8B50}";

            public static readonly Guid SolutionOpening_guid = new Guid("{D2567162-F94F-4091-8798-A096E61B8B50}");

            public const string SolutionOrProjectUpgrading_string = "{EF4F870B-7B85-4F29-9D15-CE1ABFBE733B}";

            public static readonly Guid SolutionOrProjectUpgrading_guid = new Guid("{EF4F870B-7B85-4F29-9D15-CE1ABFBE733B}");

            public const string ToolboxInitialized_string = "{DC5DB425-F0FD-4403-96A1-F475CDBA9EE0}";

            public static readonly Guid ToolboxInitialized_guid = new Guid("{DC5DB425-F0FD-4403-96A1-F475CDBA9EE0}");

            public const string VBProject_string = "{164B10B9-B200-11D0-8C61-00A0C91E29D5}";

            public static readonly Guid VBProject_guid = new Guid("{164B10B9-B200-11D0-8C61-00A0C91E29D5}");

            public const string CSharpProject_string = "{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}";

            public static readonly Guid CSharpProject_guid = new Guid("{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}");

            public const string VCProject_string = "{8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942}";

            public static readonly Guid VCProject_guid = new Guid("{8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942}");

            public const string FSharpProject_string = "{F2A71F9B-5D33-465A-A702-920D77279786}";

            public static readonly Guid FSharpProject_guid = new Guid("{F2A71F9B-5D33-465A-A702-920D77279786}");

            public const string VBCodeAttribute_string = "{C28E28CA-E6DC-446F-BE1A-D496BEF8340A}";

            public static readonly Guid VBCodeAttribute_guid = new Guid("{C28E28CA-E6DC-446F-BE1A-D496BEF8340A}");

            public const string VBCodeClass_string = "{C28E28CA-E6DC-446F-BE1A-D496BEF83401}";

            public static readonly Guid VBCodeClass_guid = new Guid("{C28E28CA-E6DC-446F-BE1A-D496BEF83401}");

            public const string VBCodeDelegate_string = "{C28E28CA-E6DC-446F-BE1A-D496BEF83402}";

            public static readonly Guid VBCodeDelegate_guid = new Guid("{C28E28CA-E6DC-446F-BE1A-D496BEF83402}");

            public const string VBCodeEnum_string = "{C28E28CA-E6DC-446F-BE1A-D496BEF83408}";

            public static readonly Guid VBCodeEnum_guid = new Guid("{C28E28CA-E6DC-446F-BE1A-D496BEF83408}");

            public const string VBCodeFunction_string = "{C28E28CA-E6DC-446F-BE1A-D496BEF83400}";

            public static readonly Guid VBCodeFunction_guid = new Guid("{C28E28CA-E6DC-446F-BE1A-D496BEF83400}");

            public const string VBCodeInterface_string = "{C28E28CA-E6DC-446F-BE1A-D496BEF83406}";

            public static readonly Guid VBCodeInterface_guid = new Guid("{C28E28CA-E6DC-446F-BE1A-D496BEF83406}");

            public const string VBCodeNamespace_string = "{C28E28CA-E6DC-446F-BE1A-D496BEF83409}";

            public static readonly Guid VBCodeNamespace_guid = new Guid("{C28E28CA-E6DC-446F-BE1A-D496BEF83409}");

            public const string VBCodeParameter_string = "{C28E28CA-E6DC-446F-BE1A-D496BEF83405}";

            public static readonly Guid VBCodeParameter_guid = new Guid("{C28E28CA-E6DC-446F-BE1A-D496BEF83405}");

            public const string VBCodeProperty_string = "{C28E28CA-E6DC-446F-BE1A-D496BEF83404}";

            public static readonly Guid VBCodeProperty_guid = new Guid("{C28E28CA-E6DC-446F-BE1A-D496BEF83404}");

            public const string VBCodeStruct_string = "{C28E28CA-E6DC-446F-BE1A-D496BEF83407}";

            public static readonly Guid VBCodeStruct_guid = new Guid("{C28E28CA-E6DC-446F-BE1A-D496BEF83407}");

            public const string VBCodeVariable_string = "{C28E28CA-E6DC-446F-BE1A-D496BEF83403}";

            public static readonly Guid VBCodeVariable_guid = new Guid("{C28E28CA-E6DC-446F-BE1A-D496BEF83403}");

            public const string BackgroundProjectLoad_string = "{dc769521-31a2-41a5-9bbb-210b5d63568d}";

            public static readonly Guid BackgroundProjectLoad_guid = new Guid("{dc769521-31a2-41a5-9bbb-210b5d63568d}");

            public const string StandardPreviewerConfigurationChanging_string = "{6D3CAD8E-9129-4ec0-929E-69B6F5D4400D}";

            public static readonly Guid StandardPreviewerConfigurationChanging_guid = new Guid("{6D3CAD8E-9129-4ec0-929E-69B6F5D4400D}");

            public const string IdeUserSignedIn_string = "{6FB82950-B2F8-4F94-9417-506703704DB2}";

            public static readonly Guid IdeUserSignedIn_guid = new Guid("{6FB82950-B2F8-4F94-9417-506703704DB2}");

            public const string SolutionHasSilverlightWindowsPhoneProject_string = "781D1330-8DE9-429D-BF73-C74F19E4FCB1";

            public const string WizardOpen_string = "{C3DA54E0-794F-440C-8655-DA03CD0DD05E}";

            public static readonly Guid WizardOpen_guid = new Guid("{C3DA54E0-794F-440C-8655-DA03CD0DD05E}");

            public const string SolutionHasWindowsPhone80NativeProject_string = "de9f6b31-c1e5-b965-95f3-1885af956fc9";

            public const string SynchronousSolutionOperation_string = "{30315F71-BB05-436B-8CC1-6A62B368C842}";

            public static readonly Guid SynchronousSolutionOperation_guid = new Guid("{30315F71-BB05-436B-8CC1-6A62B368C842}");

            public const string SharedMSBuildFilesManagerHierarchyLoaded_string = "{22912BB2-3FF9-4D55-B4DB-D210B6035D4C}";

            public static readonly Guid SharedMSBuildFilesManagerHierarchyLoaded_guid = new Guid("{22912BB2-3FF9-4D55-B4DB-D210B6035D4C}");

            public const string ShellInitialized_string = "{E80EF1CB-6D64-4609-8FAA-FEACFD3BC89F}";

            public static readonly Guid ShellInitialized_guid = new Guid("{E80EF1CB-6D64-4609-8FAA-FEACFD3BC89F}");
        }

        public static class VsTaskListView
        {
            public static readonly Guid All = new Guid("{1880202e-fc20-11d2-8bb1-00c04f8ec28c}");

            public static readonly Guid UserTasks = new Guid("{1880202f-fc20-11d2-8bb1-00c04f8ec28c}");

            public static readonly Guid ShortcutTasks = new Guid("{18802030-fc20-11d2-8bb1-00c04f8ec28c}");

            public static readonly Guid HTMLTasks = new Guid("{36ac1c0d-fe86-11d2-8bb1-00c04f8ec28c}");

            public static readonly Guid CompilerTasks = new Guid("{18802033-fc20-11d2-8bb1-00c04f8ec28c}");

            public static readonly Guid CommentTasks = new Guid("{18802034-fc20-11d2-8bb1-00c04f8ec28c}");

            public static readonly Guid CurrentFileTasks = new Guid("{18802035-fc20-11d2-8bb1-00c04f8ec28c}");

            public static readonly Guid CheckedTasks = new Guid("{18802036-fc20-11d2-8bb1-00c04f8ec28c}");

            public static readonly Guid UncheckedTasks = new Guid("{18802037-fc20-11d2-8bb1-00c04f8ec28c}");
        }

        public static class StandardToolWindows
        {
            public static readonly Guid ApplicationVerifier = new Guid("{637792AA-F332-4BB5-BE6C-066B0E88ECED}");

            public static readonly Guid Autos = new Guid("{F2E84780-2AF1-11D1-A7FA-00A0C9110051}");

            public static readonly Guid Behaviors = new Guid("{56B32054-DE4D-4de3-8396-BCB6F98BD246}");

            public static readonly Guid Bookmarks = new Guid("{A0C5197D-0AC7-4B63-97CD-8872A789D233}");

            public static readonly Guid Breakpoints = new Guid("{BE4D7042-BA3F-11D2-840E-00C04F9902C1}");

            public static readonly Guid CSSApplyStyles = new Guid("{402DC223-D700-4029-866F-ACEE803F3F0C}");

            public static readonly Guid CSSManageStyles = new Guid("{38ED9834-0C97-445b-BD1D-F78F3E08AFAC}");

            public static readonly Guid CSSProperties = new Guid("{A9B00010-7308-415c-95C6-EED62C1B9788}");

            public static readonly Guid CSSPropertyGrid = new Guid("{1CBA9826-3184-4799-A184-784E41B56398}");

            public static readonly Guid CallBrowser = new Guid("{5415EA3A-D813-4948-B51E-562082CE0887}");

            public static readonly Guid CallBrowserSecondary = new Guid("{F78BCC56-71F7-4E7D-8215-F690CAE4F452}");

            public static readonly Guid CallHierarchy = new Guid("{3822E751-EB69-4B0E-B301-595A9E4C74D5}");

            public static readonly Guid CallStack = new Guid("{0504FF91-9D61-11D0-A794-00A0C9110051}");

            public static readonly Guid ClassDetails = new Guid("{778B5376-AD77-4751-ACDC-F3D18343F8DD}");

            public static readonly Guid ClassView = new Guid("{C9C0AE26-AA77-11d2-B3F0-0000F87570EE}");

            public static readonly Guid CodeCoverageResults = new Guid("{905DA7D1-18FD-4A46-8D0F-A5FF58ADA9DE}");

            public static readonly Guid CodeDefinition = new Guid("{588470CC-84F8-4a57-9AC4-86BCA0625FF4}");

            public static readonly Guid CodeMetrics = new Guid("{9A7CEBBB-DC5C-4986-BC49-962DA46AA506}");

            public static readonly Guid ColorPalette = new Guid("{5B6781C0-E99D-11D0-9954-00A0C91BC8E5}");

            public static readonly Guid Command = new Guid("{28836128-FC2C-11D2-A433-00C04F72D18A}");

            public static readonly Guid ConditionalFormatting = new Guid("{6FB4A4D9-0C08-4663-AF7B-2ECBDF7A20EC}");

            public static readonly Guid ConsoleIO = new Guid("{FC29E0C0-C1AB-4B30-B5DF-24AA452B9661}");

            public static readonly Guid DBProEventMonitor = new Guid("{F16E7758-BFD9-4360-A45F-6DEEAE786164}");

            public static readonly Guid DataCollectionControl = new Guid("{47A7D881-D3CF-4036-B57C-0444E12DF881}");

            public static readonly Guid DataGenerationDetails = new Guid("{E3369CF0-996F-45BA-881E-2AF696FBE27B}");

            public static readonly Guid DataGenerationPreview = new Guid("{F044F2C2-3D99-4787-A492-6B09A19DF7C0}");

            public static readonly Guid DataSource = new Guid("{873151D0-CF2E-48cc-B4BF-AD0394F6A3C3}");

            public static readonly Guid DatabaseSchemaView = new Guid("{9C7D10E9-0147-4363-BF48-917F0426CD03}");

            public static readonly Guid DebugHistory = new Guid("{ed485b08-5acf-4ce9-8e13-699174ea0201}");

            public static readonly Guid DeviceSecurityManager = new Guid("{E5C2CCE5-61D0-4CD8-A946-13EC76CFDB01}");

            public static readonly Guid Disassembly = new Guid("{CF577B8C-4134-11D2-83E5-00C04F9902C1}");

            public static readonly Guid DocumentOutline = new Guid("{25F7E850-FFA1-11D0-B63F-00A0C922E851}");

            public static readonly Guid EntityMappingDetails = new Guid("{cdbdee54-b399-484b-b763-db2c3393d646}");

            public static readonly Guid EntityModelBrowser = new Guid("{A34B1C5D-6D37-4A0C-A8B0-99F8E8158B48}");

            public static readonly Guid ErrorList = new Guid("{D78612C7-9962-4B83-95D9-268046DAD23A}");

            public static readonly Guid Find1 = new Guid("{0F887920-C2B6-11d2-9375-0080C747D9A0}");

            public static readonly Guid Find2 = new Guid("{0F887921-C2B6-11d2-9375-0080C747D9A0}");

            public static readonly Guid FindInFiles = new Guid("{E830EC50-C2B5-11d2-9375-0080C747D9A0}");

            public static readonly Guid FindReplace = new Guid("{CF2DDC32-8CAD-11d2-9302-005345000000}");

            public static readonly Guid FindSymbol = new Guid("{53024D34-0EF5-11d3-87E0-00C04F7971A5}");

            public static readonly Guid FindSymbolResults = new Guid("{68487888-204A-11d3-87EB-00C04F7971A5}");

            public static readonly Guid HTMLPropertyGrid = new Guid("{F62AF5AD-1276-46dd-AE7B-D07AB54D1081}");

            public static readonly Guid Immediate = new Guid("{ECB7191A-597B-41F5-9843-03A4CF275DDE}");

            public static readonly Guid Layers = new Guid("{7B8C4981-13EC-4c56-9F24-ABE5FAAA9440}");

            public static readonly Guid LoadTest = new Guid("{CB4D394C-6408-4607-8C42-0910D3147A4E}");

            public static readonly Guid LoadTestPostRun = new Guid("{93A69444-E846-4571-9E03-A8433AD9DDF9}");

            public static readonly Guid LocalChanges = new Guid("{53544C4D-5C18-11d3-AB71-0050040AE094}");

            public static readonly Guid Locals = new Guid("{4A18F9D0-B838-11D0-93EB-00A0C90F2734}");

            public static readonly Guid MacroExplorer = new Guid("{07CD18B4-3BA1-11d2-890A-0060083196C6}");

            public static readonly Guid ManualTestExecution = new Guid("{3ADDF8E2-81CC-41A0-9785-DBD2D86064BF}");

            public static readonly Guid Modules = new Guid("{37ABA9BE-445A-11D3-9949-00C04F68FD0A}");

            public static readonly Guid ObjectBrowser = new Guid("{269A02DC-6AF8-11D3-BDC4-00C04F688E50}");

            public static readonly Guid ObjectTestBench = new Guid("{FDFFCCF2-5F63-404F-86AD-33693F544948}");

            public static readonly Guid Output = new Guid("{34E76E81-EE4A-11D0-AE2E-00A0C90FFFC3}");

            public static readonly Guid ParallelStacks = new Guid("{B9A151CE-EF7C-4fe1-A6AA-4777E6E518F3}");

            public static readonly Guid ParallelTasks = new Guid("{8D263989-FF4B-4a78-90C8-B2BA3FA69311}");

            public static readonly Guid PendingCheckIn = new Guid("{2456BD12-ECF7-4988-A4A6-67D49173F564}");

            public static readonly Guid PerformanceExplorer = new Guid("{099CA9EA-0AE4-4E31-A7E4-FE09BD1715CC}");

            public static readonly Guid Processes = new Guid("{51C76317-9037-4CF2-A20A-6206FD30B4A1}");

            public static readonly Guid Properties = new Guid("{EEFA5220-E298-11D0-8F78-00A0C9110057}");

            public static readonly Guid PropertyManager = new Guid("{6B8E94B5-0949-4d9c-A81F-C1B9B744185C}");

            public static readonly Guid Registers = new Guid("{CA4B8FF5-BFC7-11D2-9929-00C04F68FDAF}");

            public static readonly Guid ResourceView = new Guid("{2D7728C2-DE0A-45b5-99AA-89B609DFDE73}");

            public static readonly Guid RunningDocuments = new Guid("{ECDD9EE0-AC6B-11D0-89F9-00A0C9110055}");

            public static readonly Guid SQLSchemaUpdateScript = new Guid("{F2C4BE33-CA39-41a6-A69A-F4ED439D4178}");

            public static readonly Guid ServerExplorer = new Guid("{74946827-37a0-11d2-a273-00c04f8ef4ff}");

            public static readonly Guid SolutionExplorer = new Guid("{3AE79031-E1BC-11D0-8F78-00A0C9110057}");

            public static readonly Guid SourceControlExplorer = new Guid("{99B8FA2F-AB90-4F57-9C32-949F146F1914}");

            public static readonly Guid SourceHistory = new Guid("{2456BD12-ECF7-4988-A4A6-67D49173F565}");

            public static readonly Guid StartPage = new Guid("{387cb18d-6153-4156-9257-9ac3f9207bbe}");

            public static readonly Guid StyleOrganizer = new Guid("{A764E899-518D-11d2-9A89-00C04F79EFC3}");

            public static readonly Guid TaskList = new Guid("{4A9B7E51-AA16-11D0-A8C5-00A0C921A4D2}");

            public static readonly Guid TeamExplorer = new Guid("{131369F2-062D-44A2-8671-91FF31EFB4F4}");

            public static readonly Guid TestImpactView = new Guid("{0DB31CC8-2322-4f59-A610-1FDC8423DF77}");

            public static readonly Guid TestManager = new Guid("{C79B74FF-F1D7-4C94-AEFA-4D22BFE1B1F9}");

            public static readonly Guid TestResults = new Guid("{519E8A32-1C95-4A42-956F-2CEE2F28EB0F}");

            public static readonly Guid TestRunQueue = new Guid("{92547016-2BD0-4DFE-BD4F-5B52BDCE0037}");

            public static readonly Guid TestView = new Guid("{3ADDF8E2-81CC-41A0-9785-DBD2D86064BD}");

            public static readonly Guid Threads = new Guid("{E62CE6A0-B439-11D0-A79D-00A0C9110051}");

            public static readonly Guid Toolbox = new Guid("{B1E99781-AB81-11D0-B683-00AA00A3EE26}");

            public static readonly Guid UAMSynchronizations = new Guid("{A94C758F-EFB0-4975-BF86-C87B59FDB45D}");

            public static readonly Guid VCPPPropertyManager = new Guid("{DE1FC918-F32E-4DD7-A915-1792A051F26B}");

            public static readonly Guid VSMDPropertyBrowser = new Guid("{74946810-37a0-11d2-a273-00c04f8ef4ff}");

            public static readonly Guid VSTOAddBookmark = new Guid("{FF863E2F-29C9-4686-95D8-5A2D5B4D72CE}");

            public static readonly Guid Watch = new Guid("{90243340-BD7A-11D0-93EF-00A0C90F2734}");

            public static readonly Guid WebBrowser = new Guid("{e8b06f52-6d01-11d2-aa7d-00c04f990343}");

            public static readonly Guid WebBrowserPreview = new Guid("{e8b06f53-6d01-11d2-aa7d-00c04f990343}");

            public static readonly Guid WebPartGallery = new Guid("{A693A243-4743-4034-AED4-BEC4E79E0B3B}");

            public static readonly Guid XMLSchemaExplorer = new Guid("{DD1DDD20-D59B-11DA-A94D-0800200C9A66}");
        }

        public static class ReferenceManagerHandler
        {
            public const string guidRecentMenuCmdSetString = "8206e3a8-09d6-4f97-985f-7b980b672a97";

            public static readonly Guid guidRecentMenuCmdSet = new Guid("8206e3a8-09d6-4f97-985f-7b980b672a97");

            public const uint cmdidClearRecentReferences = 256u;

            public const uint cmdidRemoveFromRecentReferences = 512u;
        }

        public static class ComponentSelectorPageGuid
        {
            public const string ManagedAssemblyPage_string = "{9A341D95-5A64-11D3-BFF9-00C04F990235}";

            public static readonly Guid ManagedAssemblyPage_guid = new Guid("{9A341D95-5A64-11D3-BFF9-00C04F990235}");

            public const string COMPage_string = "{9A341D96-5A64-11D3-BFF9-00C04F990235}";

            public static readonly Guid COMPage_guid = new Guid("{9A341D96-5A64-11D3-BFF9-00C04F990235}");

            public const string ProjectsPage_string = "{9A341D97-5A64-11D3-BFF9-00C04F990235}";

            public static readonly Guid ProjectsPage_guid = new Guid("{9A341D97-5A64-11D3-BFF9-00C04F990235}");
        }

        public static class LOGVIEWID
        {
            public const string Any_string = "{FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF}";

            public static readonly Guid Any_guid = new Guid("{FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF}");

            public const string Code_string = "{7651A701-06E5-11D1-8EBD-00A0C90F26EA}";

            public static readonly Guid Code_guid = new Guid("{7651A701-06E5-11D1-8EBD-00A0C90F26EA}");

            public const string Debugging_string = "{7651A700-06E5-11D1-8EBD-00A0C90F26EA}";

            public static readonly Guid Debugging_guid = new Guid("{7651A700-06E5-11D1-8EBD-00A0C90F26EA}");

            public const string Designer_string = "{7651A702-06E5-11D1-8EBD-00A0C90F26EA}";

            public static readonly Guid Designer_guid = new Guid("{7651A702-06E5-11D1-8EBD-00A0C90F26EA}");

            public const string ProjectSpecificEditor_string = "{80A3471A-6B87-433E-A75A-9D461DE0645F}";

            public static readonly Guid ProjectSpecificEditor_guid = new Guid("{80A3471A-6B87-433E-A75A-9D461DE0645F}");

            public static readonly Guid Primary_guid = Guid.Empty;

            public const string TextView_string = "{7651A703-06E5-11D1-8EBD-00A0C90F26EA}";

            public static readonly Guid TextView_guid = new Guid("{7651A703-06E5-11D1-8EBD-00A0C90F26EA}");

            public const string UserChooseView_string = "{7651A704-06E5-11D1-8EBD-00A0C90F26EA}";

            public static readonly Guid UserChooseView_guid = new Guid("{7651A704-06E5-11D1-8EBD-00A0C90F26EA}");
        }

        public static class DebugTargetHandler
        {
            public const string guidDebugTargetHandlerCmdSetString = "6E87CFAD-6C05-4adf-9CD7-3B7943875B7C";

            public static readonly Guid guidDebugTargetHandlerCmdSet = new Guid("6E87CFAD-6C05-4adf-9CD7-3B7943875B7C");

            public const uint cmdidDebugTargetAnchorItem = 257u;

            public const uint cmdidDebugTargetAnchorItemNoAttachToProcess = 258u;

            public const uint cmdidGenericDebugTarget = 512u;

            public const uint cmdidDebugTypeCombo = 16u;

            public const uint cmdidDebugTypeItemHandler = 17u;
        }

        public static class AppPackageDebugTargets
        {
            public const string guidAppPackageDebugTargetCmdSetString = "FEEA6E9D-77D8-423F-9EDE-3970CBB76125";

            public static readonly Guid guidAppPackageDebugTargetCmdSet = new Guid("FEEA6E9D-77D8-423F-9EDE-3970CBB76125");

            public const uint cmdidAppPackage_Simulator = 256u;

            public const uint cmdidAppPackage_LocalMachine = 512u;

            public const uint cmdidAppPackage_TetheredDevice = 768u;

            public const uint cmdidAppPackage_RemoteMachine = 1024u;

            public const uint cmdidAppPackage_Emulator = 1280u;
        }

        public enum VSITEMID : uint
        {
            Nil = 4294967295u,
            Root = 4294967294u,
            Selection = 4294967293u
        }

        public enum SelectionElement : uint
        {
            UndoManager,
            WindowFrame,
            DocumentFrame,
            StartupProject,
            PropertyBrowserSID,
            UserContext,
            ResultList,
            LastWindowFrame
        }

        [Flags]
        public enum VsUIAccelModifiers : uint
        {
            VSAM_None = 0u,
            VSAM_Shift = 1u,
            VSAM_Control = 2u,
            VSAM_Alt = 4u,
            VSAM_Windows = 8u
        }

        public enum VsSearchNavigationKeys : uint
        {
            SNK_Enter,
            SNK_Down,
            SNK_Up,
            SNK_PageDown,
            SNK_PageUp,
            SNK_Home,
            SNK_End,
            SNK_Escape
        }

        public enum VsSearchTaskStatus : uint
        {
            Created,
            Started,
            Completed,
            Stopped,
            Error
        }

        public enum MessageBoxResult
        {
            IDOK = 1,
            IDCANCEL,
            IDABORT,
            IDRETRY,
            IDIGNORE,
            IDYES,
            IDNO,
            IDCLOSE,
            IDHELP,
            IDTRYAGAIN,
            IDCONTINUE
        }

        public const int cmdidToolsOptions = 264;

        public static readonly Guid IID_IUnknown = new Guid("{00000000-0000-0000-C000-000000000046}");

        public static readonly Guid GUID_AppCommand = new Guid("{12F1A339-02B9-46E6-BDAF-1071F76056BF}");

        public static readonly Guid GUID_VSStandardCommandSet97 = new Guid("{5EFC7975-14BC-11CF-9B2B-00AA00573819}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid VSStd2K = new Guid("{1496A755-94DE-11D0-8C3F-00C04FC2AAE2}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid VsStd2010 = new Guid("{5DD0BB59-7076-4C59-88D3-DE36931F63F0}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid VsStd11 = new Guid("{D63DB1F0-404E-4B21-9648-CA8D99245EC3}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid VsStd12 = new Guid("{2A8866DC-7BDE-4dc8-A360-A60679534384}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid VsStd14 = new Guid("{4C7763BF-5FAF-4264-A366-B7E1F27BA958}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public const uint CEF_CLONEFILE = 1u;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public const uint CEF_OPENFILE = 2u;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public const uint CEF_SILENT = 4u;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public const uint CEF_OPENASNEW = 8u;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_VsUIHierarchyWindowCmds = new Guid("{60481700-078B-11D1-AAF8-00A0C9055A90}");

        public static readonly IntPtr HIERARCHY_DONTCHANGE = new IntPtr(-1);

        public static readonly IntPtr SELCONTAINER_DONTCHANGE = new IntPtr(-1);

        public static readonly IntPtr HIERARCHY_DONTPROPAGATE = new IntPtr(-2);

        public static readonly IntPtr SELCONTAINER_DONTPROPAGATE = new IntPtr(-2);

        public const string MiscFilesProjectUniqueName = "<MiscFiles>";

        public const string SolutionItemsProjectUniqueName = "<SolnItems>";

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid CLSID_HtmDocData = new Guid("{62C81794-A9EC-11D0-8198-00A0C91BBEE3}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid CLSID_HtmedPackage = new Guid("{1B437D20-F8FE-11D2-A6AE-00104BCC7269}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid CLSID_HtmlLanguageService = new Guid("{58E975A0-F8FE-11D2-A6AE-00104BCC7269}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_HtmlEditorFactory = new Guid("{C76D83F8-A489-11D0-8195-00A0C91BBEE3}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_TextEditorFactory = new Guid("{8B382828-6202-11D1-8870-0000F87579D2}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_HTMEDAllowExistingDocData = new Guid("{5742D216-8071-4779-BF5F-A24D5F3142BA}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid CLSID_VsEnvironmentPackage = new Guid("{DA9FB551-C724-11D0-AE1F-00A0C90FFFC3}");

        public static readonly Guid GUID_VsNewProjectPseudoFolder = new Guid("{DCF2A94A-45B0-11D1-ADBF-00C04FB6BE4C}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid CLSID_MiscellaneousFilesProject = new Guid("{A2FE74E1-B743-11D0-AE1A-00A0C90FFFC3}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid CLSID_SolutionItemsProject = new Guid("{D1DCDB85-C5E8-11D2-BFCA-00C04F990235}");

        public static readonly Guid SID_SVsGeneralOutputWindowPane = new Guid("{65482C72-DEFA-41B7-902C-11C091889C83}");

        public static readonly Guid SID_SUIHostCommandDispatcher = new Guid("{E69CD190-1276-11D1-9F64-00A0C911004F}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid CLSID_VsUIHierarchyWindow = new Guid("{7D960B07-7AF8-11D0-8E5E-00A0C911005A}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_DefaultEditor = new Guid("{6AC5EF80-12BF-11D1-8E9B-00A0C911005A}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_ExternalEditor = new Guid("{8137C9E8-35FE-4AF2-87B0-DE3C45F395FD}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_BuildOutputWindowPane = new Guid("{1BD8A850-02D1-11d1-BEE7-00A0C913D1F8}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_OutWindowDebugPane = new Guid("{FC076020-078A-11D1-A7DF-00A0C9110051}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_OutWindowGeneralPane = new Guid("{3C24D581-5591-4884-A571-9FE89915CD64}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid BuildOrder = new Guid("2032B126-7C8D-48AD-8026-0E0348004FC0");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid BuildOutput = new Guid("1BD8A850-02D1-11D1-BEE7-00A0C913D1F8");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid DebugOutput = new Guid("FC076020-078A-11D1-A7DF-00A0C9110051");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_ItemType_PhysicalFile = new Guid("{6BB5F8EE-4483-11D3-8BCF-00C04F8EC28C}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_ItemType_PhysicalFolder = new Guid("{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_ItemType_VirtualFolder = new Guid("{6BB5F8F0-4483-11D3-8BCF-00C04F8EC28C}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_ItemType_SubProject = new Guid("{EA6618E8-6E24-4528-94BE-6889FE16485C}");

        public static readonly Guid GUID_BrowseFilePage = new Guid("2483F435-673D-4FA3-8ADD-B51442F65349");

        public static readonly Guid guidCOMPLUSLibrary = new Guid(516370391u, 51232, 17011, 154, 33, 119, 122, 92, 82, 46, 3);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid CLSID_ComPlusOnlyDebugEngine = new Guid("449EC4CC-30D2-4032-9256-EE18EB41B62B");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_ItemType_SharedProjectReference = new Guid("{FBA6BD9A-47F3-4C04-BDC0-7F76A9E2E582}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_VS_DEPTYPE_BUILD_PROJECT = new Guid("707d11b6-91ca-11d0-8a3e-00a0c91e2acd");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_ProjectDesignerEditor = new Guid("04b8ab82-a572-4fef-95ce-5222444b6b64");

        public const uint VS_BUILDABLEPROJECTCFGOPTS_REBUILD = 1u;

        public const uint VS_BUILDABLEPROJECTCFGOPTS_BUILD_SELECTION_ONLY = 2u;

        public const uint VS_BUILDABLEPROJECTCFGOPTS_BUILD_ACTIVE_DOCUMENT_ONLY = 4u;

        public const uint VS_BUILDABLEPROJECTCFGOPTS_PACKAGE = 8u;

        public const uint VS_BUILDABLEPROJECTCFGOPTS_PRIVATE = 4294901760u;

        public const uint VSUTDCF_DTEEONLY = 1u;

        public const uint VSUTDCF_REBUILD = 2u;

        public const uint VSUTDCF_PACKAGE = 4u;

        public const uint VSUTDCF_PRIVATE = 4294901760u;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid UICONTEXT_SolutionBuilding = new Guid("{adfc4e60-0397-11d1-9f4e-00a0c911004f}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid UICONTEXT_Debugging = new Guid("{adfc4e61-0397-11d1-9f4e-00a0c911004f}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid UICONTEXT_Dragging = new Guid("{b706f393-2e5b-49e7-9e2e-b1825f639b63}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid UICONTEXT_FullScreenMode = new Guid("{adfc4e62-0397-11d1-9f4e-00a0c911004f}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid UICONTEXT_DesignMode = new Guid("{adfc4e63-0397-11d1-9f4e-00a0c911004f}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid UICONTEXT_NoSolution = new Guid("{adfc4e64-0397-11d1-9f4e-00a0c911004f}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid UICONTEXT_SolutionExists = new Guid("{f1536ef8-92ec-443c-9ed7-fdadf150da82}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid UICONTEXT_EmptySolution = new Guid("{adfc4e65-0397-11d1-9f4e-00a0c911004f}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid UICONTEXT_SolutionHasSingleProject = new Guid("{adfc4e66-0397-11d1-9f4e-00a0c911004f}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid UICONTEXT_SolutionHasMultipleProjects = new Guid("{93694fa0-0397-11d1-9f4e-00a0c911004f}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid UICONTEXT_CodeWindow = new Guid("{8fe2df1d-e0da-4ebe-9d5c-415d40e487b5}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid UICONTEXT_SolutionHasAppContainerProject = new Guid("{7CAC4AE1-2E6B-4B02-A91C-71611E86F273}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_VsTaskListViewAll = new Guid("{1880202e-fc20-11d2-8bb1-00c04f8ec28c}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_VsTaskListViewUserTasks = new Guid("{1880202f-fc20-11d2-8bb1-00c04f8ec28c}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_VsTaskListViewShortcutTasks = new Guid("{18802030-fc20-11d2-8bb1-00c04f8ec28c}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_VsTaskListViewHTMLTasks = new Guid("{36ac1c0d-fe86-11d2-8bb1-00c04f8ec28c}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_VsTaskListViewCompilerTasks = new Guid("{18802033-fc20-11d2-8bb1-00c04f8ec28c}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_VsTaskListViewCommentTasks = new Guid("{18802034-fc20-11d2-8bb1-00c04f8ec28c}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_VsTaskListViewCurrentFileTasks = new Guid("{18802035-fc20-11d2-8bb1-00c04f8ec28c}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_VsTaskListViewCheckedTasks = new Guid("{18802036-fc20-11d2-8bb1-00c04f8ec28c}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_VsTaskListViewUncheckedTasks = new Guid("{18802037-fc20-11d2-8bb1-00c04f8ec28c}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid CLSID_VsTaskList = new Guid("{BC5955D5-aa0d-11d0-a8c5-00a0c921a4d2}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid CLSID_VsTaskListPackage = new Guid("{4A9B7E50-aa16-11d0-a8c5-00a0c921a4d2}");

        public static readonly Guid SID_SVsToolboxActiveXDataProvider = new Guid("{35222106-bb44-11d0-8c46-00c04fc2aae2}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid CLSID_VsDocOutlinePackage = new Guid("{21af45b0-ffa5-11d0-b63f-00a0c922e851}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid CLSID_VsCfgProviderEventsHelper = new Guid("{99913f1f-1ee3-11d1-8a6e-00c04f682e21}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_COMPlusPage = new Guid("{9A341D95-5A64-11d3-BFF9-00C04F990235}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_COMClassicPage = new Guid("{9A341D96-5A64-11d3-BFF9-00C04F990235}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid GUID_SolutionPage = new Guid("{9A341D97-5A64-11d3-BFF9-00C04F990235}");

        public const string AssemblyReferenceProvider_string = "{9A341D95-5A64-11D3-BFF9-00C04F990235}";

        public static readonly Guid AssemblyReferenceProvider_Guid = new Guid("{9A341D95-5A64-11D3-BFF9-00C04F990235}");

        public const string ProjectReferenceProvider_string = "{51ECA6BD-5AE4-43F0-AA76-DD0A7B08F40C}";

        public static readonly Guid ProjectReferenceProvider_Guid = new Guid("{51ECA6BD-5AE4-43F0-AA76-DD0A7B08F40C}");

        public const string ComReferenceProvider_string = "{4560BE15-8871-482A-801D-76AA47F1763A}";

        public static readonly Guid ComReferenceProvider_Guid = new Guid("{4560BE15-8871-482A-801D-76AA47F1763A}");

        public const string PlatformReferenceProvider_string = "{97324595-E3F9-4AA8-85B7-DC941E812152}";

        public static readonly Guid PlatformReferenceProvider_Guid = new Guid("{97324595-E3F9-4AA8-85B7-DC941E812152}");

        public const string FileReferenceProvider_string = "{7B069159-FF02-4752-93E8-96B3CADF441A}";

        public static readonly Guid FileReferenceProvider_Guid = new Guid("{7B069159-FF02-4752-93E8-96B3CADF441A}");

        public const string ConnectedServiceInstanceReferenceProvider_string = "{C18E5D73-E6D1-43AA-AC5E-58D82E44DA9C}";

        public static readonly Guid ConnectedServiceInstanceReferenceProvider_Guid = new Guid("{C18E5D73-E6D1-43AA-AC5E-58D82E44DA9C}");

        public const string SharedProjectReferenceProvider_string = "{88B47069-C019-4EEC-B69C-3C8630F83BA5}";

        public static readonly Guid SharedProjectReferenceProvider_Guid = new Guid("{88B47069-C019-4EEC-B69C-3C8630F83BA5}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid LOGVIEWID_Any = new Guid(4294967295u, 65535, 65535, 255, 255, 255, 255, 255, 255, 255, 255);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid LOGVIEWID_Primary = Guid.Empty;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid LOGVIEWID_Debugging = new Guid("{7651A700-06E5-11D1-8EBD-00A0C90F26EA}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid LOGVIEWID_Code = new Guid("{7651A701-06E5-11D1-8EBD-00A0C90F26EA}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid LOGVIEWID_Designer = new Guid("{7651A702-06E5-11D1-8EBD-00A0C90F26EA}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid LOGVIEWID_TextView = new Guid("{7651A703-06E5-11D1-8EBD-00A0C90F26EA}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Guid LOGVIEWID_UserChooseView = new Guid("{7651A704-06E5-11D1-8EBD-00A0C90F26EA}");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public const uint VSITEMID_NIL = 4294967295u;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public const uint VSITEMID_ROOT = 4294967294u;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public const uint VSITEMID_SELECTION = 4294967293u;

        public const uint VSCOOKIE_NIL = 0u;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public const uint UndoManager = 0u;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public const uint WindowFrame = 1u;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public const uint DocumentFrame = 2u;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public const uint StartupProject = 3u;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public const uint PropertyBrowserSID = 4u;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public const uint UserContext = 5u;

        public const int VS_E_PROJECTALREADYEXISTS = -2147213344;

        public const int VS_E_PACKAGENOTLOADED = -2147213343;

        public const int VS_E_PROJECTNOTLOADED = -2147213342;

        public const int VS_E_SOLUTIONNOTOPEN = -2147213341;

        public const int VS_E_SOLUTIONALREADYOPEN = -2147213340;

        public const int VS_E_PROJECTMIGRATIONFAILED = -2147213339;

        public const int VS_E_INCOMPATIBLEDOCDATA = -2147213334;

        public const int VS_E_UNSUPPORTEDFORMAT = -2147213333;

        public const int VS_E_WIZARDBACKBUTTONPRESS = -2147213313;

        public const int VS_S_PROJECTFORWARDED = 270320;

        public const int VS_S_TBXMARKER = 270321;

        public const int VS_E_INCOMPATIBLEPROJECT = -2147213309;

        public const int VS_E_INCOMPATIBLECLASSICPROJECT = -2147213308;

        public const int VS_E_INCOMPATIBLEPROJECT_UNSUPPORTED_OS = -2147213307;

        public const int VS_E_PROMPTREQUIRED = -2147213306;

        public const int VS_E_CIRCULARTASKDEPENDENCY = -2147213305;

        public const int VS_S_PROJECT_SAFEREPAIRREQUIRED = 270322;

        public const int VS_S_PROJECT_UNSAFEREPAIRREQUIRED = 270323;

        public const int VS_S_PROJECT_ONEWAYUPGRADEREQUIRED = 270324;

        public const int VS_S_INCOMPATIBLEPROJECT = 270325;

        public const uint ALL = 1u;

        public const uint SELECTED = 2u;

        public const int OLE_E_OLEVERB = -2147221504;

        public const int OLE_E_ADVF = -2147221503;

        public const int OLE_E_ENUM_NOMORE = -2147221502;

        public const int OLE_E_ADVISENOTSUPPORTED = -2147221501;

        public const int OLE_E_NOCONNECTION = -2147221500;

        public const int OLE_E_NOTRUNNING = -2147221499;

        public const int OLE_E_NOCACHE = -2147221498;

        public const int OLE_E_BLANK = -2147221497;

        public const int OLE_E_CLASSDIFF = -2147221496;

        public const int OLE_E_CANT_GETMONIKER = -2147221495;

        public const int OLE_E_CANT_BINDTOSOURCE = -2147221494;

        public const int OLE_E_STATIC = -2147221493;

        public const int OLE_E_PROMPTSAVECANCELLED = -2147221492;

        public const int OLE_E_INVALIDRECT = -2147221491;

        public const int OLE_E_WRONGCOMPOBJ = -2147221490;

        public const int OLE_E_INVALIDHWND = -2147221489;

        public const int OLE_E_NOT_INPLACEACTIVE = -2147221488;

        public const int OLE_E_CANTCONVERT = -2147221487;

        public const int OLE_E_NOSTORAGE = -2147221486;

        public const int DISP_E_UNKNOWNINTERFACE = -2147352575;

        public const int DISP_E_MEMBERNOTFOUND = -2147352573;

        public const int DISP_E_PARAMNOTFOUND = -2147352572;

        public const int DISP_E_TYPEMISMATCH = -2147352571;

        public const int DISP_E_UNKNOWNNAME = -2147352570;

        public const int DISP_E_NONAMEDARGS = -2147352569;

        public const int DISP_E_BADVARTYPE = -2147352568;

        public const int DISP_E_EXCEPTION = -2147352567;

        public const int DISP_E_OVERFLOW = -2147352566;

        public const int DISP_E_BADINDEX = -2147352565;

        public const int DISP_E_UNKNOWNLCID = -2147352564;

        public const int DISP_E_ARRAYISLOCKED = -2147352563;

        public const int DISP_E_BADPARAMCOUNT = -2147352562;

        public const int DISP_E_PARAMNOTOPTIONAL = -2147352561;

        public const int DISP_E_BADCALLEE = -2147352560;

        public const int DISP_E_NOTACOLLECTION = -2147352559;

        public const int DISP_E_DIVBYZERO = -2147352558;

        public const int DISP_E_BUFFERTOOSMALL = -2147352557;

        public const int RPC_E_CALL_REJECTED = -2147418111;

        public const int RPC_E_CALL_CANCELED = -2147418110;

        public const int RPC_E_CANTPOST_INSENDCALL = -2147418109;

        public const int RPC_E_CANTCALLOUT_INASYNCCALL = -2147418108;

        public const int RPC_E_CANTCALLOUT_INEXTERNALCALL = -2147418107;

        public const int RPC_E_CONNECTION_TERMINATED = -2147418106;

        public const int RPC_E_SERVER_DIED = -2147418105;

        public const int RPC_E_CLIENT_DIED = -2147418104;

        public const int RPC_E_INVALID_DATAPACKET = -2147418103;

        public const int RPC_E_CANTTRANSMIT_CALL = -2147418102;

        public const int RPC_E_CLIENT_CANTMARSHAL_DATA = -2147418101;

        public const int RPC_E_CLIENT_CANTUNMARSHAL_DATA = -2147418100;

        public const int RPC_E_SERVER_CANTMARSHAL_DATA = -2147418099;

        public const int RPC_E_SERVER_CANTUNMARSHAL_DATA = -2147418098;

        public const int RPC_E_INVALID_DATA = -2147418097;

        public const int RPC_E_INVALID_PARAMETER = -2147418096;

        public const int RPC_E_CANTCALLOUT_AGAIN = -2147418095;

        public const int RPC_E_SERVER_DIED_DNE = -2147418094;

        public const int RPC_E_SYS_CALL_FAILED = -2147417856;

        public const int RPC_E_OUT_OF_RESOURCES = -2147417855;

        public const int RPC_E_ATTEMPTED_MULTITHREAD = -2147417854;

        public const int RPC_E_NOT_REGISTERED = -2147417853;

        public const int RPC_E_FAULT = -2147417852;

        public const int RPC_E_SERVERFAULT = -2147417851;

        public const int RPC_E_CHANGED_MODE = -2147417850;

        public const int RPC_E_INVALIDMETHOD = -2147417849;

        public const int RPC_E_DISCONNECTED = -2147417848;

        public const int RPC_E_RETRY = -2147417847;

        public const int RPC_E_SERVERCALL_RETRYLATER = -2147417846;

        public const int RPC_E_SERVERCALL_REJECTED = -2147417845;

        public const int RPC_E_INVALID_CALLDATA = -2147417844;

        public const int RPC_E_CANTCALLOUT_ININPUTSYNCCALL = -2147417843;

        public const int RPC_E_WRONG_THREAD = -2147417842;

        public const int RPC_E_THREAD_NOT_INIT = -2147417841;

        public const int RPC_E_VERSION_MISMATCH = -2147417840;

        public const int RPC_E_INVALID_HEADER = -2147417839;

        public const int RPC_E_INVALID_EXTENSION = -2147417838;

        public const int RPC_E_INVALID_IPID = -2147417837;

        public const int RPC_E_INVALID_OBJECT = -2147417836;

        public const int RPC_S_CALLPENDING = -2147417835;

        public const int RPC_S_WAITONTIMER = -2147417834;

        public const int RPC_E_CALL_COMPLETE = -2147417833;

        public const int RPC_E_UNSECURE_CALL = -2147417832;

        public const int RPC_E_TOO_LATE = -2147417831;

        public const int RPC_E_NO_GOOD_SECURITY_PACKAGES = -2147417830;

        public const int RPC_E_ACCESS_DENIED = -2147417829;

        public const int RPC_E_REMOTE_DISABLED = -2147417828;

        public const int RPC_E_INVALID_OBJREF = -2147417827;

        public const int RPC_E_NO_CONTEXT = -2147417826;

        public const int RPC_E_TIMEOUT = -2147417825;

        public const int RPC_E_NO_SYNC = -2147417824;

        public const int RPC_E_FULLSIC_REQUIRED = -2147417823;

        public const int RPC_E_INVALID_STD_NAME = -2147417822;

        public const int CO_E_FAILEDTOIMPERSONATE = -2147417821;

        public const int CO_E_FAILEDTOGETSECCTX = -2147417820;

        public const int CO_E_FAILEDTOOPENTHREADTOKEN = -2147417819;

        public const int CO_E_FAILEDTOGETTOKENINFO = -2147417818;

        public const int CO_E_TRUSTEEDOESNTMATCHCLIENT = -2147417817;

        public const int CO_E_FAILEDTOQUERYCLIENTBLANKET = -2147417816;

        public const int CO_E_FAILEDTOSETDACL = -2147417815;

        public const int CO_E_ACCESSCHECKFAILED = -2147417814;

        public const int CO_E_NETACCESSAPIFAILED = -2147417813;

        public const int CO_E_WRONGTRUSTEENAMESYNTAX = -2147417812;

        public const int CO_E_INVALIDSID = -2147417811;

        public const int CO_E_CONVERSIONFAILED = -2147417810;

        public const int CO_E_NOMATCHINGSIDFOUND = -2147417809;

        public const int CO_E_LOOKUPACCSIDFAILED = -2147417808;

        public const int CO_E_NOMATCHINGNAMEFOUND = -2147417807;

        public const int CO_E_LOOKUPACCNAMEFAILED = -2147417806;

        public const int CO_E_SETSERLHNDLFAILED = -2147417805;

        public const int CO_E_FAILEDTOGETWINDIR = -2147417804;

        public const int CO_E_PATHTOOLONG = -2147417803;

        public const int CO_E_FAILEDTOGENUUID = -2147417802;

        public const int CO_E_FAILEDTOCREATEFILE = -2147417801;

        public const int CO_E_FAILEDTOCLOSEHANDLE = -2147417800;

        public const int CO_E_EXCEEDSYSACLLIMIT = -2147417799;

        public const int CO_E_ACESINWRONGORDER = -2147417798;

        public const int CO_E_INCOMPATIBLESTREAMVERSION = -2147417797;

        public const int CO_E_FAILEDTOOPENPROCESSTOKEN = -2147417796;

        public const int CO_E_DECODEFAILED = -2147417795;

        public const int CO_E_ACNOTINITIALIZED = -2147417793;

        public const int CO_E_CANCEL_DISABLED = -2147417792;

        public const int RPC_E_UNEXPECTED = -2147352577;

        public const int VS_E_BUSY = -2147220992;

        public const int VS_E_SPECIFYING_OUTPUT_UNSUPPORTED = -2147220991;

        public const int S_FALSE = 1;

        /// <summary>
        /// Значение возвращаемое большинством функций при успешном завершении операции
        /// </summary>
        public const int S_OK = 0;

        public const int UNDO_E_CLIENTABORT = -2147205119;

        public const int E_OUTOFMEMORY = -2147024882;

        public const int E_INVALIDARG = -2147024809;

        public const int E_FAIL = -2147467259;

        public const int E_NOINTERFACE = -2147467262;

        public const int E_NOTIMPL = -2147467263;

        public const int E_UNEXPECTED = -2147418113;

        public const int E_POINTER = -2147467261;

        public const int E_HANDLE = -2147024890;

        public const int E_ABORT = -2147467260;

        public const int E_ACCESSDENIED = -2147024891;

        public const int E_PENDING = -2147483638;

        internal const int WM_USER = 1024;

        public const int VSM_TOOLBARMETRICSCHANGE = 4178;

        public const int VSM_ENTERMODAL = 4179;

        public const int VSM_EXITMODAL = 4180;

        public const int VSM_VIRTUALMEMORYLOW = 4181;

        public const int VSM_VIRTUALMEMORYCRITICAL = 4182;

        public const int CPDN_SELCHANGED = 2304;

        public const int CPDN_SELDBLCLICK = 2305;

        public const int CPPM_INITIALIZELIST = 2309;

        public const int CPPM_QUERYCANSELECT = 2310;

        public const int CPPM_GETSELECTION = 2311;

        public const int CPPM_INITIALIZETAB = 2312;

        public const int CPPM_SETMULTISELECT = 2313;

        public const int CPPM_CLEARSELECTION = 2314;

        private VSConstants()
        {
        }
    }
}
