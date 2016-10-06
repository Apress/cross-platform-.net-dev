'Soundex.cls
Public Function Soundex(ByVal strInput As String) As String

    Const ENCODING_LENGTH = 4

    Dim strChar As String
    Dim intCharValue As Integer
    Dim intOutputLength As Integer
    Dim intLastCharValue As Integer
    Dim intLoopVar As Integer

    Dim intInputLength As Integer : intInputLength = Len(strInput) - 1
    
    'Ensure we are dealing with uppercase 
    strInput = UCase(strInput)

    'Grab the first letter
    Soundex = Left$(strInput, 1)
    intOutputLength = 1

    'Encode the remaining letters as numbers
    For intLoopVar = 2 To intInputLength
        strChar = Mid(strInput, intLoopVar, 1)

        Select Case strChar
            Case "B", "P", "F", "V"
                intCharValue = 1
            Case "C", "S", "G", "J", "K", "Q", "X", "Z"
                intCharValue = 2
            Case "D", "T"
                intCharValue = 3
            Case "L"
                intCharValue = 4
            Case "M", "N"
                intCharValue = 5
            Case "R"
                intCharValue = 6
        End Select

        If (intCharValue And (intCharValue <> intLastCharValue)) Then
            Soundex = Soundex & CStr(intCharValue)
            intOutputLength = intOutputLength + 1
            If (intOutputLength = ENCODING_LENGTH) Then Exit For
        End If

        intLastCharValue = intCharValue : intCharValue = 0
    Next

    'Add zeroes if the encoding is short
    If (intOutputLength < ENCODING_LENGTH) Then
        Soundex = Soundex & String(intInputLength - ENCODING_LENGTH, "0")
    End If

End Function