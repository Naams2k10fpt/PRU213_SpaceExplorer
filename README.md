# Space Explorer

**Space Explorer** là game 2D làm bằng Unity cho Lab 1. Người chơi điều khiển tàu vũ trụ, né asteroid, nhặt star để lấy điểm và bắn laser để phá asteroid.

## Mục Tiêu Lab

- Làm quen Unity Interface, GameObject, Transform và Component.
- Tạo nhiều scene và chuyển scene bằng `SceneManager`.
- Dùng Physics 2D, Collider và Trigger.
- Hiển thị UI cơ bản: điểm, thời gian, mạng, trạng thái vũ khí.
- Viết tài liệu mô tả game, scene, script và tiến độ.

## Ý Tưởng Game

Người chơi điều khiển tàu trong không gian. Asteroid rơi từ cạnh trên màn hình với vị trí và hướng lệch ngẫu nhiên. Người chơi nhặt star để cộng điểm, bắn laser để phá asteroid, và cố gắng sống càng lâu càng tốt.

Project có mở rộng thêm so với đề gốc:

- Hệ thống 3 mạng thay vì game over ngay ở lần va chạm đầu tiên.
- Va asteroid khi không có shield sẽ bị trừ `500` điểm, thấp nhất là `0`.
- Shield đồng/bạc/vàng tồn tại lần lượt `5s`, `10s`, `15s` và chặn 1 lần va chạm.
- Vũ khí có overheat.
- Có pause menu, countdown resume, SFX và BGM.

## Cách Chơi

| Hành động | Phím |
| --- | --- |
| Di chuyển | Chọn trong `SETTING`: `WASD` hoặc phím mũi tên |
| Bắn laser | `Space` hoặc chuột trái |
| Pause | `Esc` |
| Resume khi pause | Bấm `Esc` lần nữa, game đếm ngược 3 giây |

Luật điểm:

| Sự kiện | Điểm |
| --- | ---: |
| Nhặt star | `+100` |
| Phá asteroid bằng laser | `+10` |
| Va asteroid khi không có shield | `-500`, thấp nhất `0` |

Nếu đang có shield, asteroid chỉ làm mất shield, không trừ điểm và không mất mạng.

## Scene

### MainMenu

- Nút `PLAY` để vào Gameplay.
- Nút `INSTRUCTIONS` để xem hướng dẫn.
- Nút `SETTING` để chọn chế độ điều khiển.
- Background và asteroid trang trí bay xuống.
- Phát nhạc nền `MainmenuBGM.ogg`.

Script chính:

- `Assets/Scripts/MenuManager.cs`
- `Assets/Scripts/ButtonFeedback.cs`
- `Assets/Scripts/MusicPlayer.cs`

### Gameplay

- Player ship, asteroid, star, laser và shield.
- Score ở góc trái, timer ở góc phải.
- 3 life icon.
- Heat UI cho vũ khí.
- Pause canvas có nút `MAIN MENU` và `RESTART GAME`.
- Asteroid spawn nhanh hơn mỗi `10s`.
- Nhạc nền gameplay random giữa `BGM1.ogg`, `BGM2.ogg`, `BGM3.ogg`; hết bài sẽ chọn bài random khác và không lặp ngay bài vừa phát.

Script chính:

- `Assets/Scripts/PlayerController.cs`
- `Assets/Scripts/GameManager.cs`
- `Assets/Scripts/AsteroidSpawner.cs`
- `Assets/Scripts/AsteroidMove.cs`
- `Assets/Scripts/StarSpawner.cs`
- `Assets/Scripts/Star.cs`
- `Assets/Scripts/ShieldPickup.cs`
- `Assets/Scripts/Laser.cs`
- `Assets/Scripts/MusicPlayer.cs`

### EndGame

- Hiển thị `GAME OVER`.
- Hiển thị `Best Score` trong trận vừa chơi và thời gian chơi.
- Có nút `PLAY AGAIN`, `MAIN MENU`, `QUIT GAME`.
- Phát âm thanh `GameOver.ogg`.

Script chính:

- `Assets/Scripts/EndGameManager.cs`
- `Assets/Scripts/EndGameUI.cs`
- `Assets/Scripts/ButtonFeedback.cs`
- `Assets/Scripts/MusicPlayer.cs`

## Tính Năng Chính

### Player

- Di chuyển theo chế độ đã chọn trong Main Menu.
- Bị giới hạn trong màn hình camera.
- Bắn laser bằng `Space` hoặc chuột trái.
- Sau khi mất mạng sẽ miễn nhiễm `1.5s` và nhấp nháy.

### Overheat

- Mỗi lần bắn tăng `5%` heat.
- Khi heat đạt `90%`, phát `overheatBeep.ogg`.
- Khi heat đạt `100%`, phát `overheat.ogg` và khóa bắn.
- Khi chưa overheat, cooling là `15%/s`.
- Khi đã overheat, cooling là `25%/s`.
- Vũ khí chỉ bắn lại khi heat giảm về `0%`.

### Asteroid

- Spawn từ cạnh trên màn hình.
- Random vị trí X và hướng rơi.
- Có xoay khi bay.
- Tốc độ spawn tăng mỗi `10s`.
- Va player sẽ xử lý mất mạng/trừ điểm nếu player không có shield.

### Star Và Shield

- Star rơi từ trên xuống, nhặt được `+100`.
- Mỗi lần spawn item có `15%` cơ hội ra shield thay star.
- Nếu đã spawn 5 star liên tiếp thì lần kế tiếp chắc chắn ra shield.
- Shield tier:
  - Đồng: `60%`, tồn tại `5s`.
  - Bạc: `30%`, tồn tại `10s`.
  - Vàng: `10%`, tồn tại `15s`.
- Shield chặn đúng 1 lần va chạm rồi biến mất.

### Score Và Time

- Score trong Gameplay tăng/giảm từ từ tới giá trị mới thay vì nhảy số ngay.
- EndGame hiển thị điểm cao nhất đạt được trong trận vừa chơi.
- EndGame cũng hiển thị tổng thời gian sống của trận.

### Pause

- Bấm `Esc` để pause.
- Pause canvas có `MAIN MENU` và `RESTART GAME`.
- Bấm `Esc` khi đang pause sẽ hiện countdown `3`, `2`, `1` rồi tiếp tục chơi.

### Audio

Đã gắn các âm thanh chính:

- `MainmenuBGM.ogg` cho Main Menu.
- `BGM1.ogg`, `BGM2.ogg`, `BGM3.ogg` cho Gameplay.
- `GameOver.ogg` cho End Game.
- `(defaultweapon).ogg` khi bắn laser.
- `(coin).ogg` khi nhặt star.
- `(bubble).ogg` khi nhặt shield.
- `pop.ogg` khi mất shield.
- `(rock1).ogg` tới `(rock4).ogg` random khi phá asteroid.
- Các `impactMetal*.ogg` random khi player va asteroid không có shield.
- `(bugleBeep).ogg` khi hover button.
- `(beep).ogg` khi click button.
- `(overheatBeep).ogg` khi heat đạt 90%.
- `(overheat).ogg` khi overheat 100%.

## Cấu Trúc Project

```text
Assets/
  Audio/
    Sounds/
      BGM1.ogg
      BGM2.ogg
      BGM3.ogg
      GameOver.ogg
      MainmenuBGM.ogg
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
    ButtonFeedback.cs
    EndGameManager.cs
    EndGameUI.cs
    GameManager.cs
    Laser.cs
    MenuManager.cs
    MusicPlayer.cs
    PlayerController.cs
    ShieldPickup.cs
    Star.cs
    StarSpawner.cs
  Sprites/
```

## Cách Chạy Project

1. Mở project bằng Unity Editor.
2. Mở scene `Assets/Scenes/MainMenu.unity`.
3. Nhấn Play trong Unity.
4. Bấm `PLAY` để vào Gameplay.

Phiên bản Unity đang dùng:

```text
Unity 6000.4.4f1
```

## Ghi Chú Đối Chiếu Đề Bài

Đề gốc yêu cầu khi tàu va asteroid thì game kết thúc. Project hiện dùng hệ 3 mạng để game dễ chơi hơn. Vì vậy khi thuyết trình/nộp bài nên nói rõ đây là phần mở rộng: va asteroid vẫn bị phạt điểm và mất mạng, nhưng chỉ game over sau khi mất đủ 3 mạng.

High Score lưu bằng `PlayerPrefs` chưa triển khai. EndGame hiện hiển thị **Best Score trong trận vừa chơi**, không phải kỷ lục toàn bộ các lần chơi.

## Checklist

- [x] Main Menu scene
- [x] Gameplay scene
- [x] End Game scene
- [x] Play button và Instructions
- [x] Settings chọn `WASD` hoặc phím mũi tên
- [x] Player movement
- [x] Shooting
- [x] Weapon overheat
- [x] Asteroid spawn từ cạnh trên
- [x] Asteroid spawn nhanh hơn mỗi `10s`
- [x] Star pickup `+100`
- [x] Laser phá asteroid `+10`
- [x] Va asteroid `-500`, thấp nhất `0`
- [x] Hệ thống 3 mạng
- [x] Life icon
- [x] Miễn nhiễm `1.5s`
- [x] Shield đồng/bạc/vàng
- [x] Gameplay timer
- [x] EndGame score/time
- [x] Pause menu và countdown resume
- [x] Button hover/click feedback
- [x] Gameplay SFX
- [x] MainMenu, Gameplay và EndGame BGM/Sound
- [ ] High Score lưu bằng `PlayerPrefs`
- [ ] Test full Play Mode lần cuối
- [ ] Build Windows `.exe` và test `Quit Game`
- [ ] Thêm screenshot nếu cần nộp kèm tài liệu
