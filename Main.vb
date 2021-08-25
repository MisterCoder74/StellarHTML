Imports System.IO
Imports System.IO.File
Imports System.Text.RegularExpressions
Public Class Main
    Dim path As String
    Dim nextPath As String
    Dim temp As Integer = 0
    Dim lastsearch As String = ""
    Dim workingpath As String = Application.StartupPath
   


    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        RichTextBox1.SelectedText = "<html>" & vbCrLf & "</html>"

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

   
    

    Private Sub TestFileToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles TestFileToolStripMenuItem.Click
        temp += 1
        Dim fname As String = "tempfile0" & temp.ToString & ".html"

        RichTextBox1.SaveFile(fname, RichTextBoxStreamType.PlainText)

        Process.Start(fname)
        ToolStripStatusLabel1.Text = "Temp file " & fname & " sent to default browser."
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        RichTextBox1.SelectedText = "<head>" & vbCrLf & "</head>"
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        RichTextBox1.SelectedText = "<body>" & vbCrLf & "</body>"
    End Sub

    Private Sub StartWizardToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles StartWizardToolStripMenuItem.Click
        Dim path As String = "templates/html/html5_01_quickstart.tpl"

        Dim sr As StreamReader = File.OpenText(path)

        Dim s As String

        Do While sr.Peek() >= 0

            s = sr.ReadLine() & vbCrLf

            RichTextBox1.SelectedText = (s)

        Loop

        sr.Close()
    End Sub

    

    Private Sub TableWizardToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles TableWizardToolStripMenuItem.Click
        tableadd.Show()
    End Sub

    Private Sub ImageWizardToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ImageWizardToolStripMenuItem.Click
        imageadd.Show()
    End Sub

    Private Sub ListWizardToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ListWizardToolStripMenuItem.Click
        listcreator.Show()
    End Sub

    Private Sub SaveFileToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SaveFileToolStripMenuItem.Click
        If TabPage1.Text <> "Untitled.html" Then
            RichTextBox1.SaveFile(TabPage1.Text, RichTextBoxStreamType.PlainText)
            ToolStripStatusLabel1.Text = "File updated"
        Else
            Try
                Dim dlg As New SaveFileDialog, fName As String = String.Empty

                Do
                    With dlg
                        .Title = "Save HTML File"
                        .Filter = "HTML file (*.html)|*.html|HTML file (*.htm)|*.htm"
                        .AddExtension = True
                        .DefaultExt = "html"

                        If .ShowDialog = Windows.Forms.DialogResult.OK Then

                            fName = .FileName
                            workingpath = (System.IO.Path.GetDirectoryName(dlg.FileName))
                            RichTextBox1.SaveFile(fName, RichTextBoxStreamType.PlainText)
                            Exit Do
                            Exit Sub
                        End If
                    End With
                Loop
                TabPage1.Text = fName
                TextBox2.Text = workingpath
                ListBox2.Items.Add(System.IO.Path.GetFileName(fName))
                ToolStripStatusLabel1.Text = "File saved"
            Catch ex As Exception
                MsgBox("Save error. Cannot save file.")
                ToolStripStatusLabel1.Text = "Error saving file."
            End Try
        End If

    End Sub

    Private Sub OpenFileToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles OpenFileToolStripMenuItem.Click
        Try
            Dim dlg As OpenFileDialog = New OpenFileDialog
            dlg.Title = "Open HTML file"
            dlg.Filter = "HTML file (*.html)|*.html|HTML file (*.htm)|*.htm"
            If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                RichTextBox1.LoadFile(dlg.FileName, RichTextBoxStreamType.PlainText)
                workingpath = (System.IO.Path.GetDirectoryName(dlg.FileName))
                TextBox2.Text = workingpath
                TabPage1.Text = dlg.FileName
                ListBox2.Items.Add(System.IO.Path.GetFileName(dlg.FileName))
                ListBox2.SelectedItem = dlg.FileName
            End If
            ToolStripStatusLabel1.Text = "File opened"
        Catch ex As Exception
            MsgBox("Load error. Cannot open file.")
            ToolStripStatusLabel1.Text = "Error opening file."
        End Try

    End Sub

    Private Sub UndoToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles UndoToolStripMenuItem.Click
        RichTextBox1.Undo()
    End Sub

    Private Sub RedoToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RedoToolStripMenuItem.Click
        RichTextBox1.Redo()
    End Sub

    Private Sub FormWizardToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FormWizardToolStripMenuItem.Click
        formcreator.Show()
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        Try
            If RichTextBox1.SelectedText <> "" Then
                Clipboard.SetData(DataFormats.Text, RichTextBox1.SelectedText)

            Else
                MsgBox("Please select text to copy.", MsgBoxStyle.Information, "Copy")
            End If
        Catch ex As Exception
            MsgBox("Can't copy the selected item to Clipboard.", MsgBoxStyle.Critical, "Copy")
            ToolStripStatusLabel1.Text = "Can't copy the selected item to Clipboard."
        End Try
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PasteToolStripMenuItem.Click
        Try
            If Clipboard.ContainsText Then

                RichTextBox1.SelectedText = Clipboard.GetData(DataFormats.Text)

            Else
                MsgBox("Clipboard does not contai valid elements.", MsgBoxStyle.Information, "Paste")
            End If
        Catch ex As Exception
            MsgBox("Can't paste from Clipboard.", MsgBoxStyle.Critical, "Paste")
            ToolStripStatusLabel1.Text = "Error pasting from Clipboard."
        End Try
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
        RichTextBox1.SelectAll()
    End Sub

    Private Sub ColorChartToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ColorChartToolStripMenuItem.Click
        colorcharts.Show()
    End Sub



   

    Private Sub CenterToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CenterToolStripMenuItem.Click
        Dim selected = RichTextBox1.SelectedText
        Dim justified = "<p align=center>" & selected & "</p>"
        RichTextBox1.SelectedText = justified
    End Sub

    Private Sub LeftToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles LeftToolStripMenuItem.Click
        Dim selected = RichTextBox1.SelectedText
        Dim justified = "<p align=left>" & selected & "</p>"
        RichTextBox1.SelectedText = justified
    End Sub

    Private Sub RightToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RightToolStripMenuItem.Click
        Dim selected = RichTextBox1.SelectedText
        Dim justified = "<p align=right>" & selected & "</p>"
        RichTextBox1.SelectedText = justified
    End Sub

    Private Sub FindToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FindToolStripMenuItem.Click
        If lastsearch <> "" Then
            HighlightText(lastsearch, Color.Black, RichTextBox1)
            lastsearch = ""
        End If

        Dim strfind As String
        Dim pos As Integer
        strfind = InputBox("Insert string to find:", "Find")
        If strfind = "" Then
            Exit Sub
        End If
        HighlightText(strfind, Color.Red, RichTextBox1)
        lastsearch = strfind

    End Sub

    Private Sub ReplaceToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ReplaceToolStripMenuItem.Click
        Replace.show()
    End Sub

   

    Private Sub StripAllTagsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles StripAllTagsToolStripMenuItem.Click
        Dim htmltext = RichTextBox1.Text
        Dim strippedtext As String = Regex.Replace(htmltext, "<.*?>", "")
        RichTextBox1.Text = strippedtext
        ToolStripStatusLabel1.Text = "HTML tags have been stripped correctly."
    End Sub

    Private Sub IndentCodeToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles IndentCodeToolStripMenuItem.Click
        Dim index As Integer = 0
        Dim tagindented As String = ""
        For index = 0 To RichTextBox1.Lines.Count - 2 Step 1
            Dim tagtoindent As String = RichTextBox1.Lines(index)
            If tagtoindent.StartsWith("<tr>") Or tagtoindent.StartsWith("</tr>") Then
                tagindented = "  " & tagtoindent
                RichTextBox1.Text = RichTextBox1.Text.Replace(tagtoindent, tagindented)
            ElseIf tagtoindent.StartsWith("<td") Or tagtoindent.StartsWith("</td>") Then
                tagindented = "    " & tagtoindent
                RichTextBox1.Text = RichTextBox1.Text.Replace(tagtoindent, tagindented)
            ElseIf tagtoindent.StartsWith("<caption>") Or tagtoindent.StartsWith("</caption>") Then
                tagindented = "  " & tagtoindent
                RichTextBox1.Text = RichTextBox1.Text.Replace(tagtoindent, tagindented)
            ElseIf tagtoindent.StartsWith("<input") Or tagtoindent.StartsWith("<option") Or tagtoindent.StartsWith("</option>") Then
                tagindented = "  " & tagtoindent
                RichTextBox1.Text = RichTextBox1.Text.Replace(tagtoindent, tagindented)
            ElseIf tagtoindent.StartsWith("<li>") Or tagtoindent.StartsWith("</li>") Then
                tagindented = "  " & tagtoindent
                RichTextBox1.Text = RichTextBox1.Text.Replace(tagtoindent, tagindented)
            ElseIf tagtoindent.StartsWith("<meta") Or tagtoindent.StartsWith("<title>") Then
                tagindented = "  " & tagtoindent
                RichTextBox1.Text = RichTextBox1.Text.Replace(tagtoindent, tagindented)
            ElseIf tagtoindent.StartsWith("</meta>") Or tagtoindent.StartsWith("</title>") Then
                tagindented = "  " & tagtoindent
                RichTextBox1.Text = RichTextBox1.Text.Replace(tagtoindent, tagindented)
            ElseIf tagtoindent.StartsWith("<link") Or tagtoindent.StartsWith("<style") Or tagtoindent.StartsWith("<script") Then
                tagindented = "  " & tagtoindent
                RichTextBox1.Text = RichTextBox1.Text.Replace(tagtoindent, tagindented)
            ElseIf tagtoindent.StartsWith("</style>") Or tagtoindent.StartsWith("</script>") Then
                tagindented = "  " & tagtoindent
                RichTextBox1.Text = RichTextBox1.Text.Replace(tagtoindent, tagindented)
            ElseIf tagtoindent = "" Then

            End If
        Next
    End Sub

    Private Sub BoldToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles BoldToolStripMenuItem.Click
        Dim selected = RichTextBox1.SelectedText
        Dim justified = "<b>" & selected & "</b>"
        RichTextBox1.SelectedText = justified

    End Sub

    Private Sub ItalicToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ItalicToolStripMenuItem.Click
        Dim selected = RichTextBox1.SelectedText
        Dim justified = "<i>" & selected & "</i>"
        RichTextBox1.SelectedText = justified
    End Sub

    Private Sub UnderlinedToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles UnderlinedToolStripMenuItem.Click
        Dim selected = RichTextBox1.SelectedText
        Dim justified = "<u>" & selected & "</u>"
        RichTextBox1.SelectedText = justified
    End Sub

    Private Sub StrikedToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles StrikedToolStripMenuItem.Click
        Dim selected = RichTextBox1.SelectedText
        Dim justified = "<s>" & selected & "</s>"
        RichTextBox1.SelectedText = justified
    End Sub

    Private Sub LinkWizardToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles LinkWizardToolStripMenuItem.Click
        linkadd.Show()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        about.Show()
    End Sub

    Private Sub NewFileToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles NewFileToolStripMenuItem.Click
        Dim result As DialogResult = MessageBox.Show("Do you want to save your work?", "New File", MessageBoxButtons.YesNoCancel)
        If result = DialogResult.Cancel Then

        ElseIf result = DialogResult.No Then
            RichTextBox1.Clear()
        ElseIf result = DialogResult.Yes Then
            Try
                Dim dlg As New SaveFileDialog, fName As String = String.Empty

                Do
                    With dlg
                        .Title = "Save HTML File"
                        .Filter = "HTML file (*.html)|*.html|HTML file (*.htm)|*.htm"
                        .AddExtension = True
                        .DefaultExt = "html"

                        If .ShowDialog = Windows.Forms.DialogResult.OK Then

                            fName = .FileName
                            RichTextBox1.SaveFile(fName, RichTextBoxStreamType.PlainText)
                            Exit Do
                            Exit Sub
                        End If
                    End With
                Loop
                ToolStripStatusLabel1.Text = "File saved"
                RichTextBox1.Clear()
                TabPage1.Text = "Untitled.html"
            Catch ex As Exception
                MsgBox("Save error. Cannot save file.")
                ToolStripStatusLabel1.Text = "Error saving file."
            End Try

        End If
    End Sub

    Private Sub BootstrapSnippetsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles BootstrapSnippetsToolStripMenuItem.Click

    End Sub

    Private Sub BootstrapTemplatesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles BootstrapTemplatesToolStripMenuItem.Click
        For Each foundFile As String In My.Computer.FileSystem.GetFiles("templates/bootstrap")
            Dim fileName = System.IO.Path.GetFileName(foundFile).ToString
            templatebrowser.ListBox1.Items.Add(fileName)
            templatebrowser.TextBox2.Text = "templates/bootstrap"
        Next
        templatebrowser.Show()
    End Sub

    Private Sub HTMLTemplatesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles HTMLTemplatesToolStripMenuItem.Click
        For Each foundFile As String In My.Computer.FileSystem.GetFiles("templates/html")
            Dim fileName = System.IO.Path.GetFileName(foundFile).ToString
            templatebrowser.ListBox1.Items.Add(fileName)
            templatebrowser.TextBox2.Text = "templates/html"
        Next
        templatebrowser.Show()
    End Sub

    Private Sub SaveTemplateToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        Try
            Dim dlg As New SaveFileDialog, fName As String = String.Empty

            Do
                With dlg
                    .Title = "Save Template"
                    .Filter = "TPL file (*.tpl)|*.tpl"
                    .AddExtension = True
                    .DefaultExt = "tpl"

                    If .ShowDialog = Windows.Forms.DialogResult.OK Then

                        fName = .FileName
                        RichTextBox1.SaveFile(fName, RichTextBoxStreamType.PlainText)
                        Exit Do
                        Exit Sub
                    End If
                End With
            Loop
            ToolStripStatusLabel1.Text = "Template file saved"
        Catch ex As Exception
            MsgBox("Save error. Cannot save template.")
            ToolStripStatusLabel1.Text = "Error saving template."
        End Try
    End Sub

    Private Sub CSSSnippetsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CSSSnippetsToolStripMenuItem.Click
        For Each foundFile As String In My.Computer.FileSystem.GetFiles("templates/css")
            Dim fileName = System.IO.Path.GetFileName(foundFile).ToString
            templatebrowser.ListBox1.Items.Add(fileName)
            templatebrowser.TextBox2.Text = "templates/css"
        Next
        templatebrowser.Show()
    End Sub

    Private Sub W3CHowToReferenceOnlineToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles W3CHowToReferenceOnlineToolStripMenuItem.Click
        Process.Start("https://www.w3schools.com/howto/default.asp")
    End Sub

    Private Sub CSSTricksOnlineToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CSSTricksOnlineToolStripMenuItem.Click

        Process.Start("https://css-tricks.com/snippets/html/")
    End Sub

    Private Sub HTMLHelpToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles HTMLHelpToolStripMenuItem.Click
        Process.Start("https://www.w3schools.com/html/default.asp")
    End Sub

    Private Sub IFrameWizardToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles IFrameWizardToolStripMenuItem.Click
        iframecreator.Show()
    End Sub

    Private Sub Button21_Click(sender As System.Object, e As System.EventArgs) Handles Button21.Click
        Dim selected = RichTextBox1.SelectedText
        Dim justified = "<p align=center>" & selected & "</p>"
        RichTextBox1.SelectedText = justified
    End Sub

    Private Sub Button22_Click(sender As System.Object, e As System.EventArgs) Handles Button22.Click
        Dim selected = RichTextBox1.SelectedText
        Dim justified = "<p align=left>" & selected & "</p>"
        RichTextBox1.SelectedText = justified
    End Sub

    Private Sub Button20_Click(sender As System.Object, e As System.EventArgs) Handles Button20.Click
        Dim selected = RichTextBox1.SelectedText
        Dim justified = "<p align=right>" & selected & "</p>"
        RichTextBox1.SelectedText = justified
    End Sub

    Private Sub Button15_Click(sender As System.Object, e As System.EventArgs) Handles Button15.Click
        Dim selected = RichTextBox1.SelectedText
        Dim justified = "<b>" & selected & "</b>"
        RichTextBox1.SelectedText = justified
    End Sub

    Private Sub Button16_Click(sender As System.Object, e As System.EventArgs) Handles Button16.Click
        Dim selected = RichTextBox1.SelectedText
        Dim justified = "<u>" & selected & "</u>"
        RichTextBox1.SelectedText = justified
    End Sub

    Private Sub Button17_Click(sender As System.Object, e As System.EventArgs) Handles Button17.Click
        Dim selected = RichTextBox1.SelectedText
        Dim justified = "<i>" & selected & "</i>"
        RichTextBox1.SelectedText = justified
    End Sub

    Private Sub Button18_Click(sender As System.Object, e As System.EventArgs) Handles Button18.Click
        Dim selected = RichTextBox1.SelectedText
        Dim justified = "<s>" & selected & "</s>"
        RichTextBox1.SelectedText = justified
    End Sub

    Private Sub Button9_Click(sender As System.Object, e As System.EventArgs) Handles Button9.Click
        RichTextBox1.SelectedText = "<br>"
    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        RichTextBox1.SelectedText = "<p>" & vbCrLf & "</p>"
    End Sub

    Private Sub Button10_Click(sender As System.Object, e As System.EventArgs) Handles Button10.Click
        RichTextBox1.SelectedText = "<hr>"
    End Sub

    Private Sub Button19_Click(sender As System.Object, e As System.EventArgs) Handles Button19.Click
        RichTextBox1.SelectedText = "<div>" & vbCrLf & "</div>"

    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        Dim FilePath As String = "resources/metastructure.txt"
        RichTextBox1.SelectedText = System.IO.File.ReadAllText(FilePath) & vbCrLf
    End Sub

    Private Sub Button14_Click(sender As System.Object, e As System.EventArgs) Handles Button14.Click
        RichTextBox1.SelectedText = "<title>" & vbCrLf & "</title>"
    End Sub

    Private Sub SuperscriptToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SuperscriptToolStripMenuItem.Click
        Dim selected = RichTextBox1.SelectedText
        Dim formatted = "<sup>" & selected & "</sup>"
        RichTextBox1.SelectedText = formatted
    End Sub

    Private Sub SubscriptToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SubscriptToolStripMenuItem.Click
        Dim selected = RichTextBox1.SelectedText
        Dim formatted = "<sub>" & selected & "</sub>"
        RichTextBox1.SelectedText = formatted
    End Sub

    Private Sub Main_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim lines() As String = IO.File.ReadAllLines("resources/taglist.txt")
        ListBox1.Items.AddRange(lines)
        
    End Sub

    Private Sub ListBox1_DoubleClick(sender As System.Object, e As System.EventArgs) Handles ListBox1.DoubleClick
        RichTextBox1.SelectedText = ListBox1.SelectedItem.ToString & vbCrLf
        ToolStripStatusLabel1.Text = "Tag added"
    End Sub

    Private Sub HelpFileToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles HelpFileToolStripMenuItem.Click
        Process.Start("StellarHelp.html")
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        RichTextBox1.SelectedText = "<img src=" & Chr(34) & "(put image url here)" & Chr(34) & ">"
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        Dim selected = RichTextBox1.SelectedText
        Dim linked = "<a href=" & Chr(34) & "(put link url here)" & Chr(34) & " target=" & Chr(34) & "(insert _blank here to load in new tab)" & Chr(34) & ">" & selected & "</a>"
        RichTextBox1.SelectedText = linked
    End Sub

    Private Sub Button11_Click(sender As System.Object, e As System.EventArgs) Handles Button11.Click
        RichTextBox1.SelectedText = "<table width=" & Chr(34) & "(put width in pixels here)" & Chr(34) & " align=" & Chr(34) & "(put alignment here: center, right or left)" & Chr(34) & " border=" & Chr(34) & "(put border in pixels here)" & Chr(34) & ">" & vbCrLf
        RichTextBox1.SelectedText = "<tr>" & vbCrLf & "<td align=" & Chr(34) & "(put alignment here: center, right or left)" & Chr(34) & "> (copy as meny tr and td you wish) </td>" & vbCrLf & "</tr>" & vbCrLf
        RichTextBox1.SelectedText = "</table>"


    End Sub


    Private Sub JavascriptKitToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles JavascriptKitToolStripMenuItem.Click
        Process.Start("http://www.javascriptkit.com/")
    End Sub


   

    

    Private Sub ListBox2_DoubleClick(sender As System.Object, e As System.EventArgs) Handles ListBox2.DoubleClick
        RichTextBox1.LoadFile(workingpath & "\" & ListBox2.SelectedItem, RichTextBoxStreamType.PlainText)
        TabPage1.Text = workingpath & "\" & ListBox2.SelectedItem
    End Sub

    Private Sub Button13_Click(sender As System.Object, e As System.EventArgs) Handles Button13.Click
        ListBox2.Items.Clear()
        ToolStripStatusLabel1.Text = "File list deleted"
    End Sub

    Private Sub FontWizardToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FontWizardToolStripMenuItem.Click
        Fontwizard.Show()
    End Sub


    Private Sub ToLowercaseToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ToLowercaseToolStripMenuItem.Click
        RichTextBox1.SelectedText = LCase(RichTextBox1.SelectedText)
        ToolStripStatusLabel1.Text = "Text converted to lowercase"
    End Sub

    Private Sub ToUppercaseToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ToUppercaseToolStripMenuItem.Click
        RichTextBox1.SelectedText = UCase(RichTextBox1.SelectedText)
        ToolStripStatusLabel1.Text = "Text converted to uppercase"
    End Sub

    Private Sub InlineQuoteToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles InlineQuoteToolStripMenuItem.Click
        RichTextBox1.SelectedText = "<q>" & RichTextBox1.SelectedText & "</q>"
    End Sub

    Private Sub BlockQuoteToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles BlockQuoteToolStripMenuItem.Click
        RichTextBox1.SelectedText = "<blockquote>" & RichTextBox1.SelectedText & "</blockquote>"
    End Sub

    Private Sub PreformattedToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PreformattedToolStripMenuItem.Click
        RichTextBox1.SelectedText = "<pre>" & RichTextBox1.SelectedText & "</pre>"
    End Sub

    Private Sub CodeToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CodeToolStripMenuItem.Click
        RichTextBox1.SelectedText = "<code>" & RichTextBox1.SelectedText & "</code>"
    End Sub

    Private Sub AddressToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AddressToolStripMenuItem.Click
        RichTextBox1.SelectedText = "<address>" & RichTextBox1.SelectedText & "</address>"
    End Sub

    Private Sub DatetimeToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DatetimeToolStripMenuItem.Click
        RichTextBox1.SelectedText = "<time datetime=" & Chr(34) & "(out format here)" & Chr(34) & ">" & RichTextBox1.SelectedText & "</time>"
    End Sub

    Private Sub KeyboardToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles KeyboardToolStripMenuItem.Click
        RichTextBox1.SelectedText = "<kbd>" & RichTextBox1.SelectedText & "</kbd>"
    End Sub

    Private Sub ReverseTextToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ReverseTextToolStripMenuItem.Click
        RichTextBox1.SelectedText = "<bdo  dir=" & Chr(34) & "rtl" & Chr(34) & ">" & RichTextBox1.SelectedText & "</bdo>"
    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        RichTextBox1.SelectedText = "<ol>" & vbCrLf
        RichTextBox1.SelectedText = "<li> (copy as many li as you wish) </li>" & vbCrLf
        RichTextBox1.SelectedText = "</ol>" & vbCrLf
    End Sub

    
    Private Sub ClearAllToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ClearAllToolStripMenuItem.Click
        Dim result As DialogResult = MessageBox.Show("By clicking OK your content will be lost.", "Clear All", MessageBoxButtons.OKCancel)
        If result = DialogResult.Cancel Then

        ElseIf result = DialogResult.OK Then
            RichTextBox1.Clear()
            TabPage1.Text = "Untitled.html"
        End If
    End Sub

    

    Private Sub JavascriptTemplatesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles JavascriptTemplatesToolStripMenuItem.Click
        For Each foundFile As String In My.Computer.FileSystem.GetFiles("templates/javascript")
            Dim fileName = System.IO.Path.GetFileName(foundFile).ToString
            templatebrowser.ListBox1.Items.Add(fileName)
            templatebrowser.TextBox2.Text = "templates/javascript"
        Next
        templatebrowser.Show()
    End Sub

    Private Sub FontAndStyleToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FontAndStyleToolStripMenuItem.Click
        Try
            Dim dlgfont As FontDialog = New FontDialog
            dlgfont.Font = RichTextBox1.Font
            If dlgfont.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                RichTextBox1.SelectionFont = dlgfont.Font
                RichTextBox1.Font = dlgfont.Font
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem2.Click
        Try
            Dim dlgcol As ColorDialog = New ColorDialog
            dlgcol.Color = RichTextBox1.ForeColor
            If dlgcol.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                RichTextBox1.SelectionColor = dlgcol.Color
                RichTextBox1.ForeColor = dlgcol.Color

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BackgroundColorToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles BackgroundColorToolStripMenuItem.Click
        Try
            Dim dlgcol As ColorDialog = New ColorDialog
            dlgcol.Color = RichTextBox1.ForeColor
            If dlgcol.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                RichTextBox1.BackColor = dlgcol.Color

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ToggleURLsDetectionToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ToggleURLsDetectionToolStripMenuItem.Click
        If RichTextBox1.DetectUrls = True Then
            RichTextBox1.DetectUrls = False
        ElseIf RichTextBox1.DetectUrls = False Then
            RichTextBox1.DetectUrls = True
        End If
    End Sub

   

    Private Sub XToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles XToolStripMenuItem.Click
        RichTextBox1.ZoomFactor = 2.0F
    End Sub

    Private Sub XToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles XToolStripMenuItem1.Click
        RichTextBox1.ZoomFactor = 1.5F
    End Sub

    Private Sub XToolStripMenuItem2_Click(sender As System.Object, e As System.EventArgs) Handles XToolStripMenuItem2.Click
        RichTextBox1.ZoomFactor = 1.0F
    End Sub

  
    Public Sub HighlightText(ByVal text As String, ByVal color As Drawing.Color, ByVal obj As RichTextBox)
        Dim regex As Regex
        Dim AllMatches As MatchCollection

        Dim SelStart As Int32 = 0
        Dim SelLength As Int32 = 0

        ' remember the previous selection.
        SelStart = obj.SelectionStart
        SelLength = obj.SelectionLength

        ' create our regular expression to find all matches.
        regex = New Regex("\b" & text & "\b|" & text)
        ' iterate through all the matches, highlighting as we go.
        AllMatches = regex.Matches(obj.Text)
        For Each match As Match In AllMatches
            obj.SelectionStart = match.Index
            obj.SelectionLength = text.Length
            obj.SelectionColor = color
        Next

        ' reset the previous selection.
        obj.SelectionStart = SelStart
        obj.SelectionLength = SelLength
    End Sub

    Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
        HighlightText(TextBox1.Text, Color.Red, RichTextBox1)
        lastsearch = TextBox1.Text
    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox1_MouseClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles TextBox1.MouseClick
        HighlightText(lastsearch, Color.Black, RichTextBox1)
        TextBox1.Clear()
    End Sub

    Private Sub RichTextBox1_Click(sender As System.Object, e As System.EventArgs) Handles RichTextBox1.Click
        If lastsearch <> "" Then
            HighlightText(lastsearch, Color.Black, RichTextBox1)
            lastsearch = ""
        End If
       
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem3.Click
        Try
            Dim dlg As New SaveFileDialog, fName As String = String.Empty

            Do
                With dlg
                    .Title = "Save HTML File"
                    .Filter = "HTML file (*.html)|*.html|HTML file (*.htm)|*.htm"
                    .AddExtension = True
                    .DefaultExt = "html"

                    If .ShowDialog = Windows.Forms.DialogResult.OK Then

                        fName = .FileName
                        workingpath = (System.IO.Path.GetDirectoryName(dlg.FileName))
                        RichTextBox1.SaveFile(fName, RichTextBoxStreamType.PlainText)
                        Exit Do
                        Exit Sub
                    End If
                End With
            Loop
            TabPage1.Text = fName
            TextBox2.Text = workingpath
            ListBox2.Items.Add(System.IO.Path.GetFileName(fName))
            ToolStripStatusLabel1.Text = "File saved"
        Catch ex As Exception
            MsgBox("Save error. Cannot save file.")
            ToolStripStatusLabel1.Text = "Error saving file."
        End Try
    End Sub

   

   
End Class
