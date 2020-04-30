Public Class HiColorPicker

    Public SelectedColor1 As Integer = 0
    Public SelectedColor2 As Integer = 0
    Public SelectedShade As Integer = 0
    Public SelectedItem As String

    Public Shared BoxLength As Integer = "=========[HiColor Color Picker]=========".Length
    Public Shared boxleft As Integer = (80 - BoxLength) / 2
    Public Shared SliderLength As Integer = "-------|--------".Length

    Public Function PickColor() As String

        SelectedItem = "SLIDER1"
        Render()

        Dim Key As ConsoleKeyInfo

        Do
            Key = Console.ReadKey(True)

            Select Case SelectedItem
                'Gee maybe on the next rework I'll make all of these actual objects :hmmmmmm:
                'At the rate I'm going that'll be probably within next week
                'if not then hello future me.
                Case "SLIDER1"
                    If Key.Key = ConsoleKey.LeftArrow Then
                        SelectedColor1 = Math.Max(0, SelectedColor1 - 1)
                    ElseIf Key.Key = ConsoleKey.RightArrow Then
                        SelectedColor1 = Math.Min(15, SelectedColor1 + 1)
                    ElseIf Key.Key = ConsoleKey.DownArrow Or Key.Key = ConsoleKey.Enter Or Key.Key = ConsoleKey.Tab Then
                        SelectedItem = "SLIDER2"
                    End If

                Case "SLIDER2"
                    If Key.Key = ConsoleKey.LeftArrow Then
                        SelectedColor2 = Math.Max(0, SelectedColor2 - 1)
                    ElseIf Key.Key = ConsoleKey.RightArrow Then
                        SelectedColor2 = Math.Min(15, SelectedColor2 + 1)
                    ElseIf Key.Key = ConsoleKey.DownArrow Or Key.Key = ConsoleKey.Enter Or Key.Key = ConsoleKey.Tab Then
                        If Key.Modifiers = ConsoleModifiers.Shift Then SelectedItem = "SLIDER1" Else SelectedItem = "SLIDER3"
                    ElseIf Key.Key = ConsoleKey.UpArrow Then
                        SelectedItem = "SLIDER1"
                    End If

                Case "SLIDER3"
                    If Key.Key = ConsoleKey.LeftArrow Then
                        SelectedShade = Math.Max(0, SelectedShade - 1)
                    ElseIf Key.Key = ConsoleKey.RightArrow Then
                        SelectedShade = Math.Min(2, SelectedShade + 1)
                    ElseIf Key.Key = ConsoleKey.DownArrow Or Key.Key = ConsoleKey.Enter Or Key.Key = ConsoleKey.Tab Then
                        If Key.Modifiers = ConsoleModifiers.Shift Then SelectedItem = "SLIDER2" Else SelectedItem = "OKBTN"
                    ElseIf Key.Key = ConsoleKey.UpArrow Then
                        SelectedItem = "SLIDER2"
                    End If

                Case "OKBTN"
                    If Key.Key = ConsoleKey.UpArrow Then SelectedItem = "SLIDER3"
                    If Key.Key = ConsoleKey.RightArrow Then SelectedItem = "CANCELBTN"
                    If Key.Key = ConsoleKey.Enter Then
                        clear()
                        Return ConstructColorString()
                    End If

                Case "CANCELBTN"
                    If Key.Key = ConsoleKey.UpArrow Then SelectedItem = "SLIDER3"
                    If Key.Key = ConsoleKey.LeftArrow Then SelectedItem = "OKBTN"
                    If Key.Key = ConsoleKey.Enter Then
                        Clear()
                        Return Nothing
                    End If

            End Select

            PartialRender()

        Loop

        Pause()
        Return ConstructColorString()

    End Function


    Public Sub Render()

        Box(ConsoleColor.Gray, BoxLength, 10, boxleft, 3)
        Sprite("=========[HiColor Color Picker]=========", ConsoleColor.Black, ConsoleColor.White, boxleft, 3)
        Sprite("Color 1:", ConsoleColor.Gray, ConsoleColor.Black, boxleft + 1, 3 + 2)
        Sprite("Color 2:", ConsoleColor.Gray, ConsoleColor.Black, boxleft + 1, 3 + 5)
        Sprite("Shade: ░▒▓", ConsoleColor.Gray, ConsoleColor.Black, boxleft + 28, 3 + 2)
        Sprite("Prev:", ConsoleColor.Gray, ConsoleColor.Black, boxleft + 28, 3 + 5)

        SetPos(boxleft + 10, 3 + 2)
        Draw("0123456789ABCDEF")

        SetPos(boxleft + 10, 3 + 5)
        Draw("0123456789ABCDEF")

        PartialRender()

    End Sub

    Private Sub PartialRender()

        Dim BG As ConsoleColor

        'Slider1
        If SelectedItem = "SLIDER1" Then BG = ConsoleColor.DarkBlue Else BG = ConsoleColor.DarkGray
        Row(BG, SliderLength, boxleft + 10, 3 + 3)
        Block(ConsoleColor.DarkRed, boxleft + 10 + SelectedColor1, 3 + 3)

        'Slider 2
        If SelectedItem = "SLIDER2" Then BG = ConsoleColor.DarkBlue Else BG = ConsoleColor.DarkGray
        Row(BG, SliderLength, boxleft + 10, 3 + 6)
        Block(ConsoleColor.DarkRed, boxleft + 10 + SelectedColor2, 3 + 6)

        'Slider 3
        If SelectedItem = "SLIDER3" Then BG = ConsoleColor.DarkBlue Else BG = ConsoleColor.DarkGray
        Row(BG, 3, boxleft + BoxLength - 5, 3 + 3)
        Block(ConsoleColor.DarkRed, boxleft + BoxLength - 5 + SelectedShade, 3 + 3)

        'Preview box
        SetPos(boxleft + BoxLength - 5, 3 + 5)
        Dim Colorstring As String = ConstructColorString()
        HiColorDraw(String.Join("-", {Colorstring, Colorstring, Colorstring}))

        If SelectedItem = "OKBTN" Then BG = ConsoleColor.DarkBlue Else BG = ConsoleColor.DarkGray
        Sprite("[     OK     ]", BG, ConsoleColor.White, boxleft + 3, 3 + 8)

        If SelectedItem = "CANCELBTN" Then BG = ConsoleColor.DarkBlue Else BG = ConsoleColor.DarkGray
        Sprite("[   CANCEL   ]", BG, ConsoleColor.White, boxleft + 24, 3 + 8)

    End Sub

    Private Function ConstructColorString() As String
        Return Hex(SelectedColor1) & Hex(SelectedColor2) & SelectedShade
    End Function

    Public Sub clear()
        Box(ConsoleColor.DarkBlue, BoxLength, 10, boxleft, 3)
    End Sub

    '=========[HiColor Color Picker]=========
    '
    ' Color 1: 0123456789ABCDEF  Shade: 012
    '          -------|--------         -|-  
    '
    ' Color 2: 0123456789ABCDEF  Prev.  [ ] 
    '          -------|--------
    '
    '   [     OK     ]       [   CANCEL   ]
    '

End Class
