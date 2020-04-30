Imports System.IO
Imports Henja2.BasicRender
Module Module1
    Public Currentdocument As String()
    Public CurrentX As Integer
    Public CurrentY As Integer
    Public TopDisplayY As Integer
    Public TopDisplayX As Integer

    Sub Main()
        Console.SetWindowSize(80, 25)
        Console.SetBufferSize(80, 25)
        Color(ConsoleColor.DarkBlue, ConsoleColor.White)
        Console.Clear()

        Row(ConsoleColor.Black, 80, 0, 0)
        Row(ConsoleColor.Black, 79, 0, 24)
        Color(ConsoleColor.Black, ConsoleColor.White)
        SetPos(0, 24)
        CenterText("Henja Editor v2.0")
        SetPos(0, 0)
        Sprite("FILE", ConsoleColor.Black, ConsoleColor.White, 0, 0)
        SetPos(0, 1)
        CurrentX = 0
        CurrentY = 0

        Dim Args() As String
        Args = Environment.GetCommandLineArgs()
        Console.Title = String.Join(" ", Args)
        If Args.Count = 2 Then
            If Args(1).ToUpper.EndsWith(".AB") Then

                Exit Sub
            ElseIf Args(1).ToUpper.EndsWith(".DF") Then
                DrawFromFile(Args(1), 0, 0)
                Exit Sub
            Else
                FileOpen(1, Args(1), OpenMode.Input)
                Dim Linecounter As Integer
                Linecounter = 0
                While Not EOF(1)
                    ReDim Preserve Currentdocument(Linecounter)
                    Currentdocument(Linecounter) = LineInput(1)
                    Linecounter = Linecounter + 1
                End While
                FileClose(1)
                RenderDocument()
            End If
        Else
            ReDim Currentdocument(0)
            Currentdocument(0) = ""
        End If



        Type()

    End Sub

    Sub Type()
        Dim CurrentKey As ConsoleKeyInfo
        Do
            Color(ConsoleColor.DarkBlue, ConsoleColor.White)
            CurrentKey = Console.ReadKey()
            If CurrentKey.Modifiers = ConsoleModifiers.Control Then
                Select Case CurrentKey.Key
                    Case ConsoleKey.S
                    'Save
                    Case ConsoleKey.O
                    'Open
                    Case ConsoleKey.N
                        'New
                End Select
            Else
                Select Case CurrentKey.Key
                    Case ConsoleKey.Enter
                        CurrentY = CurrentY + 1
                        If Currentdocument.Count = CurrentY Then
                            ReDim Preserve Currentdocument(CurrentY)
                        Else
                            ReDim Preserve Currentdocument(Currentdocument.Count)
                            For X = Currentdocument.Count - 1 To CurrentY Step -1
                                Currentdocument(X) = Currentdocument(X - 1)
                            Next
                            RenderDocument()
                        End If
                        UpdatePos()
                    Case ConsoleKey.LeftArrow
                        Sprite(Currentdocument(CurrentY)(CurrentX), ConsoleColor.DarkBlue, ConsoleColor.White)
                        CurrentX = CurrentX - 1
                        If CurrentX = -1 Then CurrentX = 0
                        UpdatePos()
                    Case ConsoleKey.RightArrow
                        Sprite(Currentdocument(CurrentY)(CurrentX), ConsoleColor.DarkBlue, ConsoleColor.White)
                        CurrentX = CurrentX + 1
                        If CurrentX = Currentdocument(CurrentY).Count Then CurrentX = CurrentX - 1
                        UpdatePos()
                    Case ConsoleKey.UpArrow
                        Sprite(Currentdocument(CurrentY)(CurrentX), ConsoleColor.DarkBlue, ConsoleColor.White)
                        CurrentY = CurrentY - 1
                        If CurrentY = -1 Then CurrentY = 0
                        UpdatePos()
                    Case ConsoleKey.DownArrow
                        Sprite(Currentdocument(CurrentY)(CurrentX), ConsoleColor.DarkBlue, ConsoleColor.White)
                        CurrentY = CurrentY + 1
                        If CurrentY = Currentdocument.Count Then CurrentY = CurrentY - 1
                        UpdatePos()
                    Case Else
                        If CurrentX = Currentdocument(CurrentY).Count Then
                            Currentdocument(CurrentY) = Currentdocument(CurrentY) & CurrentKey.KeyChar
                        Else
                            Currentdocument(CurrentY) = Currentdocument(CurrentY).Remove(CurrentX, 1).Insert(CurrentX, CurrentKey.KeyChar)
                        End If
                        CurrentX = CurrentX + 1
                        UpdatePos()
                End Select
            End If


        Loop
    End Sub
    Sub RenderDocument()
        Dim y As Integer
        For y = TopDisplayY To TopDisplayY + 22
            Row(ConsoleColor.DarkBlue, 80, 0, y + 1)
            SetPos(0, y + 1)
            Color(ConsoleColor.DarkBlue, ConsoleColor.White)
            If y = Currentdocument.Count Then
                Exit For
            End If
            If Currentdocument(y).Length > 0 Then
                For X = TopDisplayX To Currentdocument(y).Length - 1
                    If X > 79 + TopDisplayX Then
                        Exit For
                    End If
                    Console.Write(Currentdocument(y)(X))
                Next
            End If
        Next
    End Sub

    Sub UpdatePos()
        Sprite(CurrentX & "," & CurrentY, ConsoleColor.Black, ConsoleColor.White, 0, 24)
        SetPos(CurrentX - TopDisplayX, CurrentY - TopDisplayY + 1)
        Try
            Sprite(Currentdocument(CurrentY)(CurrentX), ConsoleColor.Green, ConsoleColor.White)
        Catch
            Sprite(" ", ConsoleColor.Green, ConsoleColor.White)
        End Try


    End Sub

End Module
