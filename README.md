# Space Explorer

**Space Explorer** là một game 2D làm bằng Unity cho Lab 1. Người chơi điều khiển một tàu vũ trụ, né asteroid, nhặt star để lấy điểm và bắn laser để phá asteroid.

## Mục tiêu

Mục tiêu của project là luyện tập các kiến thức Unity cơ bản:

- Làm quen giao diện Unity và cách tạo scene.
- Thao tác với GameObject.
- Di chuyển object bằng Transform.
- Sử dụng Component trong Unity.
- Dùng Physics 2D và Collider.
- Tạo và quản lý nhiều scene.
- Làm UI cơ bản và hiển thị điểm số.

## Ý tưởng game

Người chơi điều khiển tàu vũ trụ trong không gian. Các star xuất hiện từ phía trên màn hình và người chơi có thể nhặt để cộng điểm. Asteroid cũng xuất hiện và bay qua màn hình. Người chơi có thể bắn laser để phá asteroid và nhận thêm điểm.

Người chơi có 3 mạng. Mỗi lần tàu va chạm với asteroid sẽ mất một mạng; game kết thúc khi mất mạng thứ ba. Shield item có thể xuất hiện thay cho star và chặn một lần va chạm. Sau đó game chuyển sang scene End Game để hiển thị điểm và thời gian cuối cùng.

## Cách điều khiển

Các phím hiện tại phụ thuộc vào chế độ điều khiển được chọn trong `SETTINGS`:

| Hành động | Phím |
| --- | --- |
| Di chuyển ở chế độ WASD | `W`, `A`, `S`, `D` |
| Di chuyển ở chế độ Arrow Keys | Các phím mũi tên |
| Bắn laser | `Space` hoặc chuột trái |

Ghi chú:
- Main Menu có nút `SETTINGS` để chọn một trong hai chế độ điều khiển: `WASD` hoặc phím mũi tên. Mỗi lần chơi chỉ dùng chế độ đang được chọn.
- Chế độ đã chọn được lưu bằng `PlayerPrefs` và áp dụng khi vào Gameplay.

## Các scene trong game

### MainMenu

Scene Main Menu gồm:

- Tên game.
- Nút Play.
- Nút Instructions.
- Nút Settings để chọn chế độ điều khiển.
- Panel hướng dẫn chơi.
- Panel cài đặt điều khiển.
- Nút đóng panel hướng dẫn.

Script chính:
- `Assets/Scripts/MenuManager.cs`

Các object UI của Settings đã được đặt trực tiếp trong Hierarchy của scene `MainMenu`.

### Gameplay

Scene Gameplay gồm:

- Tàu của người chơi.
- Bộ sinh asteroid.
- Bộ sinh star.
- Shield item đồng, bạc và vàng.
- UI điểm số ở góc trái.
- Ba life icon ở dưới điểm số.
- UI nhiệt vũ khí ở giữa.
- UI thời gian ở góc phải.
- Background.
- Game manager.

Scripts chính:
- `Assets/Scripts/PlayerController.cs`
- `Assets/Scripts/GameManager.cs`
- `Assets/Scripts/AsteroidSpawner.cs`
- `Assets/Scripts/AsteroidMove.cs`
- `Assets/Scripts/StarSpawner.cs`
- `Assets/Scripts/Star.cs`
- `Assets/Scripts/Laser.cs`

### EndGame

Scene End Game gồm:

- Dòng chữ Game Over.
- Điểm cuối cùng và thời gian chơi cuối cùng trên cùng một hàng.
- Nút Play Again.
- Nút Main Menu.
- Nút Quit Game đã nối với hàm `QuitGame()` trong `EndGameManager.cs`.

Scripts chính:
- `Assets/Scripts/EndGameManager.cs`
- `Assets/Scripts/EndGameUI.cs`

## Cách tính điểm

| Sự kiện | Điểm |
| --- | ---: |
| Nhặt một star | `+100` |
| Phá một asteroid bằng laser | `+10` |
| Va chạm asteroid khi không có shield | `-500`, thấp nhất là `0` |

Khi player va chạm với asteroid mà không có shield, player bị trừ `500` điểm, thấp nhất là `0`, và mất một mạng. Game lưu điểm hiện tại và chuyển sang scene End Game khi player mất mạng thứ ba. Nếu đang có shield, shield sẽ chặn va chạm một lần nên player không mất điểm và không mất mạng.

Score trong Gameplay dùng hiệu ứng **animated score counter**: khi điểm thay đổi, số trên UI sẽ tăng hoặc giảm dần tới giá trị mới thay vì nhảy ngay lập tức.

## Prefab

| Prefab | Mục đích |
| --- | --- |
| `Assets/Prefabs/Star.prefab` | Vật phẩm để nhặt và cộng điểm |
| `Assets/Prefabs/Shield.prefab` | Shield pickup được cấu hình theo tier khi spawn |
| `Assets/Prefabs/Asteroid.prefab` | Vật cản trong game |
| `Assets/Prefabs/Laser.prefab` | Đạn laser do player bắn ra |

## Mô tả script

### PlayerController.cs

Xử lý di chuyển của player, giới hạn player trong màn hình, bắn laser, hệ thống overheat và miễn nhiễm `1.5s` sau khi mất mạng.

Luật overheat:
- Mỗi phát bắn tăng `5%` nhiệt.
- Đạt `100%` thì vũ khí bị khóa và hiện `OVERHEATED!` màu đỏ.
- Khi chưa overheat, kể cả lúc đang bắn, nhiệt giảm `15%` mỗi giây.
- Khi đã overheat, nhiệt giảm nhanh hơn ở mức `25%` mỗi giây.
- Vũ khí chỉ được mở lại khi nhiệt giảm hoàn toàn xuống `0%`.

### GameManager.cs

Quản lý điểm, hiệu ứng animated score counter, thời gian, 3 mạng, life icon, trừ `500` điểm khi player va chạm asteroid, kết quả cuối và chuyển sang scene End Game khi player mất mạng thứ ba.

### MenuManager.cs

Xử lý các nút trong Main Menu, bao gồm bắt đầu game, hiện/ẩn panel hướng dẫn, hiện/ẩn panel settings và lưu chế độ điều khiển bằng `PlayerPrefs`.

### AsteroidSpawner.cs

Sinh asteroid ngẫu nhiên từ cạnh trên của vùng gameplay. Vị trí X và hướng rơi được random để asteroid rơi xuống nhưng vẫn có độ lệch trái/phải. Sau mỗi 30 giây, tốc độ spawn asteroid tăng lên bằng cách giảm thời gian giữa các lần spawn.

### AsteroidMove.cs

Di chuyển asteroid theo hướng được random, xoay asteroid, xóa asteroid khi ra khỏi màn hình và kích hoạt xử lý mất mạng khi asteroid va chạm với player.

### StarSpawner.cs

Sinh item từ phía trên màn hình theo thời gian. Mỗi lần spawn có `15%` xác suất tạo shield thay cho star; nếu đã có 5 star liên tiếp thì lần tiếp theo được bảo đảm là shield. Trong số shield drop, đồng/bạc/vàng có tỉ lệ `60% / 30% / 10%`.

### Star.cs

Di chuyển star xuống dưới, xóa star khi ra khỏi màn hình và cộng điểm khi player nhặt được star.

### ShieldPickup.cs

Di chuyển shield xuống dưới và cấp một lớp bảo vệ cho player. Shield đồng tồn tại `5s`, bạc `10s`, vàng `15s`; shield chặn một lần va asteroid rồi biến mất. Visual quanh tàu dùng sprite tĩnh và đã được canh thủ công trong scene.

### Laser.cs

Di chuyển laser lên trên, xóa laser khi ra khỏi màn hình và phá asteroid khi va chạm.

### EndGameManager.cs

Xử lý các chức năng ở scene End Game: chơi lại, về Main Menu và thoát game bằng hàm `QuitGame()`.

### EndGameUI.cs

Hiển thị điểm cuối cùng và thời gian chơi cuối cùng ở scene End Game.

## Tiến độ hiện tại

Vòng lặp gameplay chính đã gần hoàn thành:

- Đã có scene Main Menu.
- Đã có scene Gameplay.
- Đã có scene End Game.
- Player có thể di chuyển và bắn.
- Vũ khí có hệ thống overheat và tự làm nguội.
- Player có 3 mạng, life icon, bị trừ `500` điểm khi va asteroid và miễn nhiễm `1.5s` sau va chạm.
- Shield đồng, bạc và vàng có thể drop và chặn một lần sát thương.
- Star được sinh ra và có thể nhặt.
- Asteroid được sinh ra và có thể va chạm với player.
- Asteroid spawn nhanh hơn sau mỗi 30 giây.
- Laser có thể phá asteroid.
- Điểm được hiển thị trong gameplay.
- Thời gian chơi được hiển thị ở góc phải trong gameplay.
- Điểm cuối và thời gian chơi được hiển thị sau khi game over.
- Chuyển scene đã được cài đặt.

Các việc nên cải thiện:

- Test toàn bộ flow trong Unity Editor.
- Thêm high score bằng `PlayerPrefs`.
- Thêm gameplay/UI SFX bằng audio asset đã có.
- Thêm ảnh chụp màn hình nếu cần nộp tài liệu.

## Cấu trúc project

```text
Assets/
  Prefabs/
    Asteroid.prefab
    Laser.prefab
    Shield.prefab
    Star.prefab
  Scenes/
    MainMenu.unity
    Gameplay.unity
    EndGame.unity
  Scripts/
    AsteroidMove.cs
    AsteroidSpawner.cs
    EndGameManager.cs
    EndGameUI.cs
    GameManager.cs
    Laser.cs
    MenuManager.cs
    PlayerController.cs
    ShieldPickup.cs
    Star.cs
    StarSpawner.cs
  Sprites/
    Backgrounds/
    PNG/
```

## Cách chạy project

1. Mở project bằng Unity Editor.
2. Mở scene `Assets/Scenes/MainMenu.unity`.
3. Nhấn Play.
4. Bấm nút `PLAY` để vào scene Gameplay.

Phiên bản Unity của project:

```text
Unity 6000.4.4f1
```

## Checklist nộp bài

- [x] Scene Main Menu
- [x] Scene Gameplay
- [x] Scene End Game
- [x] Player movement
- [x] Shooting
- [x] Weapon overheat system
- [ ] Verify three-lives system in Play Mode
- [ ] Verify bronze, silver and gold shields in Play Mode
- [x] Asteroids
- [x] Asteroid random movement
- [x] Asteroid spawn speed increases every 30 seconds
- [x] Stars
- [x] Scoring
- [x] Gameplay timer
- [x] End Game final time display
- [x] Scene transitions
- [x] README documentation
- [x] Hỗ trợ phím mũi tên
- [x] Nút Quit trong End Game
- [ ] Test lần cuối trong Unity Play Mode
