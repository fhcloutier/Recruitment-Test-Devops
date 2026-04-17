# If you don't have access to powershell on your machine, please do not hesitate to fetch the docker image to do this exercice.
# The code below is an example on how to fetch the json from the endpoint.
# Please provide the proper code to have a display that matches the question.
$albums = Invoke-WebRequest -URI http://jsonplaceholder.typicode.com/albums | ConvertFrom-Json 
$photos = Invoke-WebRequest -URI http://jsonplaceholder.typicode.com/photos | ConvertFrom-Json 

$photosPerAlbum = $photos | 
Group-Object -Property albumId -noElement |
Sort-Object -Property @{Expression = "Count"; Descending = $true}, @{Expression = {$_.Name -as [int]}; Descending = $false}

$topAlbums = $photosPerAlbum | Select-Object -First 5

$hashAlbums=@{}
foreach ($album in $albums) {
  $hashAlbums[[int]$album.id] = $album
}

class Album {
    [int]$AlbumId
    [string]$AlbumTitle
    [int]$PhotoCount;
}

$albumList = @()

$topAlbums | foreach {
  $other = $hashAlbums[[int]$_.Name]

  $albumList += [Album]@{AlbumId=$_.Name
                   AlbumTitle=$other.title
                  PhotoCount=$_.Count}
}

Write-Output $albumList