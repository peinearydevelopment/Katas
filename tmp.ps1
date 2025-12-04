# for ($i = 1; $i -le 31; $i++) {
#     $day = if ($i -lt 10) { "0$($i)" } else { "$($i)" }
#     $solution = "Dec.01.24.1".Replace("01", $day)
#     $solutionLocation = ".\$($solution)"
#     Copy-Item -Path ".\Dec.01.24.1" -Destination "$($solutionLocation)" -Recurse

#     $filePath = "$solutionLocation\Test1.cs"
#     $fileContent = Get-Content -Path $filePath
#     $updatedContent = $fileContent -replace "OldToken", "NewToken"
#     Set-Content -Path $filePath -Value $updatedContent
# }

# Define the source directory
$sourceName = "Dec.01.24.1"
$sourceDir = ".\$sourceName"

# Define the parent directory where the copies will be created
$destinationParent = ".\"

# Loop through each day in December
for ($day = 1; $day -le 31; $day++) {
    # Format the day as two digits
    $dayFormatted = "{0:D2}" -f $day

    # Define the names for the two copies
    $firstCopyName = "Dec.$dayFormatted.24.1"
    $secondCopyName = "Dec.$dayFormatted.24.2"

    # Define the paths for the two copies
    $firstCopyPath = Join-Path -Path $destinationParent -ChildPath $firstCopyName
    $secondCopyPath = Join-Path -Path $destinationParent -ChildPath $secondCopyName

    # Create the first copy
    if ($firstCopyPath -ne $sourceDir) {
        Copy-Item -Path $sourceDir -Destination $firstCopyPath -Recurse
        # Write-Host $firstCopyPath

        $filePath = "$firstCopyPath\Test1.cs"
        $fileContent = Get-Content -Path $filePath
        $updatedContent = $fileContent -replace $sourceName.Replace(".", "._"), $firstCopyName.Replace(".", "._")
        Set-Content -Path $filePath -Value $updatedContent

        $filePath = "$firstCopyPath\$sourceName.csproj"
        $fileContent = Get-Content -Path $filePath
        $updatedContent = $fileContent -replace $sourceName.Replace(".", "._"), $firstCopyName.Replace(".", "._")
        Set-Content -Path $filePath -Value $updatedContent
        Rename-Item -Path $filePath -NewName "$firstCopyName.csproj"

        dotnet sln "AdventOfCode2024.sln" add "$firstCopyName\$firstCopyName.csproj"
    }

    # Create the second copy
    Copy-Item -Path $sourceDir -Destination $secondCopyPath -Recurse
    # Write-Host $secondCopyPath
    $filePath = "$secondCopyPath\Test1.cs"
    $fileContent = Get-Content -Path $filePath
    $updatedContent = $fileContent -replace $sourceName.Replace(".", "._"), $secondCopyName.Replace(".", "._")
    Set-Content -Path $filePath -Value $updatedContent

    $filePath = "$secondCopyPath\$sourceName.csproj"
    $fileContent = Get-Content -Path $filePath
    $updatedContent = $fileContent -replace $sourceName.Replace(".", "._"), $secondCopyName.Replace(".", "._")
    Set-Content -Path $filePath -Value $updatedContent
    Rename-Item -Path $filePath -NewName "$secondCopyName.csproj"

    dotnet sln "AdventOfCode2024.sln" add "$secondCopyName\$secondCopyName.csproj"
}

Write-Output "Directories copied and renamed for all days in December."
