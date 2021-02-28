# SWAGGEAR_JWT_ASP.NETCORE_3.1

## Swagger + JWT
![Image of Swagger](https://raw.githubusercontent.com/Sonlanggtu/SWAGGEAR_JWT_ASP.NETCORE_3.1/main/Swagger.PNG)

## JWT

## B1: Khi login thành công sẽ generate ra token
## B2: LƯu token ở LocalStorage, SessionStorage
## B3: Mỗi lần call API sẽ đính kèm token vào phần Header Authorization [Bearer + token] => Nhờ đó mà biết được người này là ai? có quyền gì ? và tài nguyên của người này ?
## Lúc call lên api JWT tự nhận ra được các thông tin và gán chúng vào claims, (Với tích hợp 3th Nuget Identity Token thì tự gán thông tin vào claims, nếu Không dùng Indentity Token thì cần viết Middleware để gán thông tin vào claims ) nếu cần check thêm thông tin thì sẽ lấy thông tin ở claims để check với các mục đích khác nhau.

