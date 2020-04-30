Imports System.IO

Module Main
    Public Currentdocument As String()
    Public Filename As String

    Public CurrentX As Integer
    Public CurrentY As Integer

    Public EditorMode As IHenjaEditorMode

    Sub Main()
        Console.SetWindowSize(80, 25)
        Console.SetBufferSize(80, 25)
        Color(ConsoleColor.DarkBlue, ConsoleColor.White)
        Console.Clear()

        DrawHenjaHeader()

        CurrentX = 0
        CurrentY = 0

        Dim Args() As String
        Args = Environment.GetCommandLineArgs()
        Console.Title = "Henja BasicRender Editor " & String.Join(" ", Args)


        If Args.Count = 2 Then

            If Not File.Exists(Args(1)) Then
                DialogBox("Could not find file", 3, 1, True, True)
                Return
            End If

            Filename = Args(1)
            Currentdocument = GetFileContents(Args(1))

            If Not File.Exists(Args(1)) Then
                DialogBox("Specified file does not exist", 3, 1, True, True)
                Exit Sub
            End If


            If Args(1).ToUpper.EndsWith(".HC") Then
                EditorMode = New HFEditor()
                Type()
            ElseIf Args(1).ToUpper.EndsWith(".DF") Then
                EditorMode = New DFEditor()
                Type()
            End If

        Else
            Dim NewWindow As NewFileWindow = New NewFileWindow()
            Dim TempDoc As String() = NewWindow.NewHenjaFile()

            If Not IsNothing(TempDoc) Then
                EditorMode = NewWindow.Editors(NewWindow.CurrentEditorIndex)
                Filename = NewWindow.Filename
                Currentdocument = TempDoc
                Type()
            End If
        End If
    End Sub

    Public Function GetFileContents(File As String) As String()
        FileOpen(1, File, OpenMode.Input)
        Dim PageContents() As String
        Dim I As Integer = 0
        While Not EOF(1)
            ReDim Preserve PageContents(I)
            PageContents(I) = LineInput(1)
            I += 1
        End While
        FileClose(1)
        Return PageContents
    End Function

    Sub Type()

        Dim CurrentKey As ConsoleKeyInfo

        DrawHenjaHeader()
        EditorMode.Render()

        Do

            SetPos(CurrentX, CurrentY + 1)
            CurrentKey = Console.ReadKey(True)
            If CurrentKey.Modifiers = ConsoleModifiers.Control Then
                Select Case CurrentKey.Key
                    Case ConsoleKey.S
                        FileOpen(1, Filename, OpenMode.Output)
                        For X = 0 To Currentdocument.Count - 1
                            PrintLine(1, Currentdocument(X))
                        Next
                        FileClose(1)
                End Select
            Else
                Select Case CurrentKey.Key
                    Case ConsoleKey.LeftArrow
                        CurrentX -= 1
                        If CurrentX = -1 Then CurrentX = 0

                    Case ConsoleKey.RightArrow
                        CurrentX += 1
                        If CurrentX = Currentdocument(CurrentY).Count Then CurrentX -= 1
                    Case ConsoleKey.UpArrow
                        CurrentY -= 1
                        If CurrentY = -1 Then CurrentY = 0
                    Case ConsoleKey.DownArrow
                        CurrentY += 1
                        If CurrentY = Currentdocument.Count Then CurrentY -= 1
                    Case Else
                        EditorMode.KeyPress(CurrentKey)
                End Select
            End If
        Loop
    End Sub


    Private Sub DrawHenjaHeader()
        Row(ConsoleColor.Black, 80, 0, 0)
        Row(ConsoleColor.Black, 79, 0, 24)
        Color(ConsoleColor.Black, ConsoleColor.White)
        SetPos(0, 24)
        CenterText("Henja Editor v2.0")
        SetPos(0, 0)
        Sprite("CTRL+S to save, Draw with 1-F and move using arrow keys.", ConsoleColor.Black, ConsoleColor.White, 0, 0)
        SetPos(0, 1)
    End Sub

End Module
