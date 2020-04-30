Public Class NewFileWindow

    'haha I'm too lazy to measure these out aren't I a great developer??????
    Public Shared BoxLength As Integer = "==================[New File]================".Length
    Public Shared boxleft As Integer = (80 - BoxLength) / 2
    Public Shared TextboxLength As Integer = "[__________________________]".Length

    Public Filename As String = ""

    Public Editors As IHenjaEditorMode() = {
        New DFEditor,
        New HFEditor,
        New ABEditor
    }

    Public CurrentEditorIndex As Integer = 0

    Private X As String = ""
    Private Y As String = ""
    Private SelectedElement As String

    Public Function NewHenjaFile() As String()

        SelectedElement = "NAMEBOX"
        Render()

        Dim Key As ConsoleKeyInfo

        Do
            Key = Console.ReadKey(True)

            Select Case SelectedElement
                Case "NAMEBOX"
                    If Key.Key = ConsoleKey.Backspace Then
                        If Filename.Length > 0 Then Filename = Filename.Remove(Filename.Length - 1)
                    ElseIf Key.Key = ConsoleKey.DownArrow Or Key.Key = ConsoleKey.Tab Or Key.Key = ConsoleKey.Enter Then
                        SelectedElement = "EDITORBOX"
                    Else
                        Filename &= Key.KeyChar
                    End If

                Case "EDITORBOX"
                    If Key.Key = ConsoleKey.LeftArrow Then
                        CurrentEditorIndex -= 1
                        If CurrentEditorIndex = -1 Then CurrentEditorIndex = Editors.Length - 1
                    ElseIf Key.Key = ConsoleKey.RightArrow Then
                        CurrentEditorIndex += 1
                        If CurrentEditorIndex = Editors.Length Then CurrentEditorIndex = 0
                    ElseIf Key.Key = ConsoleKey.DownArrow Or Key.Key = ConsoleKey.Tab Or Key.Key = ConsoleKey.Enter Then
                        If Key.Modifiers = ConsoleModifiers.Shift Then SelectedElement = "NAMEBOX" Else SelectedElement = "XBOX"
                    ElseIf Key.Key = ConsoleKey.UpArrow Then
                        SelectedElement = "NAMEBOX"
                    End If

                Case "XBOX"
                    If Char.IsDigit(Key.KeyChar) Then
                        X &= Key.KeyChar
                        If X > 80 Then X = 80
                    ElseIf Key.Key = ConsoleKey.Backspace Then
                        If X.Length > 0 Then X = X.Remove(X.Length - 1)
                    ElseIf Key.Key = ConsoleKey.DownArrow Or Key.Key = ConsoleKey.Tab Or Key.Key = ConsoleKey.Enter Then
                        If Key.Modifiers = ConsoleModifiers.Shift Then SelectedElement = "EDITORBOX" Else SelectedElement = "YBOX"
                    ElseIf Key.Key = ConsoleKey.UpArrow Then
                        SelectedElement = "EDITORBOX"
                    End If

                Case "YBOX"
                    If Char.IsDigit(Key.KeyChar) Then
                        Y &= Key.KeyChar
                        If Y > 25 Then Y = 25
                    ElseIf Key.Key = ConsoleKey.Backspace Then
                        If Y.Length > 0 Then Y = Y.Remove(Y.Length - 1)
                    ElseIf Key.Key = ConsoleKey.DownArrow Or Key.Key = ConsoleKey.Tab Or Key.Key = ConsoleKey.Enter Then
                        If Key.Modifiers = ConsoleModifiers.Shift Then SelectedElement = "XBOX" Else SelectedElement = "OKBTN"
                    ElseIf Key.Key = ConsoleKey.UpArrow Then
                        SelectedElement = "XBOX"
                    End If

                Case "OKBTN"
                    If Key.Key = ConsoleKey.RightArrow Then SelectedElement = "CANCELBTN"
                    If Key.Key = ConsoleKey.Enter Then
                        Clear()
                        Return Editors(CurrentEditorIndex).GenerateNew(X, Y)
                    End If

                Case "CANCELBTN"
                    If Key.Key = ConsoleKey.LeftArrow Then SelectedElement = "OKBTN"
                    If Key.Key = ConsoleKey.Enter Then
                        Clear()
                        Return Nothing
                    End If

            End Select

            PartialRender()
        Loop



    End Function

    Public Sub Render()

        '==================[New File]================
        ' 
        ' Name of File: [__________________________]
        '
        ' Mode of File: [__________________________] (Will be selectable with left-right
        '
        ' Size of File: [] x []
        '
        '     [     OK     ]       [   CANCEL   ]
        '



        Box(ConsoleColor.Gray, BoxLength, 10, boxleft, 3)
        Sprite("==================[New File]================", ConsoleColor.Black, ConsoleColor.White, boxleft, 3)
        Sprite("Name of File:", ConsoleColor.Gray, ConsoleColor.Black, boxleft + 1, 3 + 2)
        Sprite("Mode of File:", ConsoleColor.Gray, ConsoleColor.Black, boxleft + 1, 3 + 4)
        Sprite("Size of File:", ConsoleColor.Gray, ConsoleColor.Black, boxleft + 1, 3 + 6)

        Sprite("X", ConsoleColor.Gray, ConsoleColor.Black, boxleft + 18, 3 + 6)

        PartialRender()

    End Sub

    Private Sub PartialRender()
        'Buttons
        Dim BGColor As ConsoleColor
        If SelectedElement = "OKBTN" Then BGColor = ConsoleColor.DarkBlue Else BGColor = ConsoleColor.DarkGray
        Sprite("[     OK     ]", BGColor, ConsoleColor.White, boxleft + 5, 3 + 8)

        If SelectedElement = "CANCELBTN" Then BGColor = ConsoleColor.DarkBlue Else BGColor = ConsoleColor.DarkGray
        Sprite("[   CANCEL   ]", BGColor, ConsoleColor.White, boxleft + 26, 3 + 8)

        'Textboxes
        If SelectedElement = "NAMEBOX" Then BGColor = ConsoleColor.DarkBlue Else BGColor = ConsoleColor.DarkGray
        Row(BGColor, TextboxLength, boxleft + 15, 3 + 2)

        Dim TrimmedFileName = Filename
        If TrimmedFileName.Length > TextboxLength Then TrimmedFileName = TrimmedFileName.Substring(TrimmedFileName.Length - 30)

        Sprite(TrimmedFileName, BGColor, ConsoleColor.White, boxleft + 15, 3 + 2)

        If SelectedElement = "EDITORBOX" Then BGColor = ConsoleColor.DarkBlue Else BGColor = ConsoleColor.DarkGray
        Row(BGColor, TextboxLength, boxleft + 15, 3 + 4)
        Sprite(Editors(CurrentEditorIndex).GetName, BGColor, ConsoleColor.White, boxleft + 15, 3 + 4)

        If SelectedElement = "XBOX" Then BGColor = ConsoleColor.DarkBlue Else BGColor = ConsoleColor.DarkGray
        Row(BGColor, 2, boxleft + 15, 3 + 6)
        Sprite(X, BGColor, ConsoleColor.White, boxleft + 15, 3 + 6)

        If SelectedElement = "YBOX" Then BGColor = ConsoleColor.DarkBlue Else BGColor = ConsoleColor.DarkGray
        Row(BGColor, 2, boxleft + 20, 3 + 6)
        Sprite(Y, BGColor, ConsoleColor.White, boxleft + 20, 3 + 6)

    End Sub

    Public Sub Clear()
        Box(ConsoleColor.DarkBlue, BoxLength, 10, (80 - BoxLength) / 2, 3)
    End Sub

End Class
