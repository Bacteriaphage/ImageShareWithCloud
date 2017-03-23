# ImageShareWithCloud
implement a Image uploading website and deploy it to Window Azure without SSL certificate. 
It support account management and Image uploading, peering and deleting.
There are four kinds of user roles: normal user, approver(approver all uploaded image), supervisor(can check image log) and admin(delete user and their images).

backend: C#

frontend: Razor

## FrameWork

### Worker Role

#### User account
Controller: AccountController

Model: IntityModel

View: Views/Account/

The user account is generally managed by MVC.NET default method. The only thing I do is to add a delete account method with a role "admin". When an admin want to delete a user, this method will delete all image infos in database and images in blobstorage.

The delete method use AntiForgerytoken to avoid CSRF attack.  

I also design a delete view in the view part.

#### Image management
Controller: ImageController

Model: Image

View: View/Image/

This controller implement following function:
Upload images and info
Query images and info
Edit images info
Delete images and info
List all images
List images by uploader
List images by Tag
Approve images (for approver only)
Check images log (for supervisor only)

All post method in this controller has AntiForgeryToken to avoid CSRF attack

Upload method upload all images and their information to cloud with a message sended onto a queue storage say "ValidationQueue".

Edit and Delete can be only done by the uploader, checking the uploader and now user.

Check images log will get image log from the azure table storage.

### WebRole

WebRole will listen to the service bus and read the validationQueue. Each time it grabs a validate request, it call a function to judge whether the image in the blob storage is jpeg type. If not, it will delete the image and its information from the database.

*we can easily do the type check in the upload method in WebRole, but it will make the web role overload.*
