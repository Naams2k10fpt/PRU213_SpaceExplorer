# Space Explorer - Kiểm tra tiến độ

Cập nhật lần cuối: 28/06/2026

File này dùng để theo dõi tiến độ thực hiện Lab 1: xây dựng game 2D Unity tên **Space Explorer**.

## Đối chiếu đề bài Lab 1

Nguồn đối chiếu: `D:\FPT_Course\PRU213\PRU212 or PRU213\Lab\Lab 1.docx`

### Tóm tắt yêu cầu gốc của đề

**Mục tiêu bài Lab**

- Tạo game 2D Unity tên **Space Explorer**.
- Luyện tập Unity Interface, Navigation, GameObject, Transform, Component System, Scene Creation/Management, Physics và Colliders.

**Game concept**

- Người chơi điều khiển tàu vũ trụ trong không gian.
- Tàu né asteroid và thu thập star để lấy điểm.
- Game có nhiều scene.

**Game elements**

- Spaceship/player:
  - Là object 2D do người chơi điều khiển.
  - Di chuyển mọi hướng bằng phím mũi tên.
  - Bắn laser.
- Asteroids:
  - Là object 2D trong không gian.
  - Di chuyển ngẫu nhiên trong scene.
  - Va chạm với asteroid làm trừ điểm.
- Stars:
  - Là object 2D xuất hiện trong không gian.
  - Nhặt star sẽ cộng điểm.

**Game flow**

- Main Menu:
  - Có nút Play để chuyển sang Gameplay.
  - Có nút Instructions để hiện panel hướng dẫn.
- Gameplay:
  - Có spaceship, asteroids và stars.
  - Người chơi né asteroid và nhặt star.
  - Game kết thúc nếu tàu va chạm asteroid.
  - Có UI hiển thị điểm.
- End Game:
  - Hiển thị score của người chơi.
  - Có lựa chọn quay về Main Menu hoặc Quit Game.

**Lab assignment**

- Tạo Main Menu scene với Play và Instructions.
- Implement player controls bằng phím mũi tên.
- Instantiate asteroids và stars ngẫu nhiên.
- Implement collision logic giữa spaceship, asteroids và stars.
- Implement scoring dựa trên collected stars.
- Implement scene transition giữa Main Menu, Gameplay và End Game.

**Rubric chấm điểm**

- Functionality `50 điểm`: movement/shooting, asteroid random movement, collision/scoring, scene transitions.
- Creativity `20 điểm`: thiết kế spaceship/asteroids/stars và thẩm mỹ tổng thể.
- Documentation `30 điểm`: code có documentation tốt, giải thích rõ từng game element và scene.

### Bảng đối chiếu yêu cầu với project hiện tại

| Yêu cầu đề bài | Trạng thái project hiện tại | Đối chiếu/sai khác cần chú ý |
| --- | --- | --- |
| Game 2D tên `Space Explorer` | Đã làm | Project, README và progress đều dùng tên/chủ đề này |
| Có nhiều scene | Đã làm | Có `MainMenu`, `Gameplay`, `EndGame`; chưa có nhiều gameplay environment khác nhau, nhưng đủ theo flow đề bài |
| Main Menu có nút Play | Đã làm | `PlayGame()` chuyển sang `Gameplay` |
| Main Menu có nút Instructions | Đã làm | Có `InstructionPanel`, `ShowInstructions()` và `CloseInstructions()` |
| Player là tàu vũ trụ 2D | Đã làm | Có player ship sprite trong scene `Gameplay` |
| Player di chuyển mọi hướng bằng phím mũi tên | Đã làm | Settings cho phép chọn phím mũi tên hoặc `WASD`; mặc định là phím mũi tên |
| Player bắn laser | Đã làm | Bắn bằng `Space` hoặc chuột trái; có thêm overheat là mở rộng |
| Có asteroid 2D | Đã làm | Có `Asteroid.prefab`, `AsteroidSpawner.cs`, `AsteroidMove.cs` |
| Asteroid di chuyển ngẫu nhiên | Đã làm | Asteroid spawn từ cạnh trên, random vị trí X, hướng rơi, tốc độ và xoay |
| Va chạm asteroid làm trừ điểm | Đã chỉnh theo đề | Khi không có shield, player va asteroid bị trừ `500` điểm, thấp nhất là `0` |
| Game kết thúc khi tàu chạm asteroid | **Sai khác có chủ đích** | Đề flow nói game over ngay, project hiện giữ hệ 3 mạng theo yêu cầu mở rộng |
| Có star 2D | Đã làm | Có `Star.prefab`, `StarSpawner.cs`, `Star.cs` |
| Stars scattered/random trong không gian | Đạt cơ bản, có thể cải thiện | Star spawn random theo X và rơi từ trên xuống; nếu chấm sát chữ `scattered`, nên cho star spawn/di chuyển ngẫu nhiên hơn |
| Nhặt star cộng điểm | Đã làm | Nhặt star `+100` |
| UI hiển thị score | Đã làm | `ScoreText` ở góc trái gameplay |
| End Game hiển thị score | Đã làm | End Game hiển thị `Score` và mở rộng thêm `Time` |
| End Game có lựa chọn về Main Menu | Đã làm | `MainMenu()` load scene `MainMenu` |
| End Game có Quit Game | Đã làm, cần build test | `QuitGame()` dùng `Application.Quit()`; cần test trong bản Windows build |
| Scene transition giữa các scene | Đã làm cơ bản | Dùng `SceneManager.LoadScene()` trực tiếp; nếu cần "smooth transition" đúng nghĩa thì nên thêm fade |
| Collision logic spaceship/asteroid/star | Đã triển khai, cần Play Mode test | Logic đã có, nhưng full flow chưa được tick test cuối |
| Documentation/code documentation | Gần đạt | README/progress đã rõ; comment trong code còn ít, nên thêm comment ngắn ở các logic chính |
| Creativity/aesthetic | Vượt mức cơ bản | Có background, sprite, overheat, timer, 3 mạng, shield tier, tăng độ khó |

### Các điểm chưa đạt hoặc sai khác cần chỉnh trước khi nộp

| Mức ưu tiên | Vấn đề | Hiện trạng | Hướng chỉnh đề xuất |
| --- | --- | --- | --- |
| Đã xử lý | Asteroid collision cần trừ điểm | Đề yêu cầu trừ điểm khi va asteroid | Đã thêm luật trừ `500` điểm, thấp nhất `0`, khi player bị asteroid hit mà không có shield |
| Cao | Game over ngay khi chạm asteroid khác với hệ 3 mạng | Đề flow nói chạm asteroid là game end, project dùng 3 mạng | Có thể giữ 3 mạng như bản mở rộng, nhưng README/progress phải giải thích rõ; nếu muốn bám sát đề tuyệt đối thì đổi về 1 mạng |
| Trung bình | Star chưa thật sự scattered/random movement | Star rơi thẳng từ trên xuống | Thêm drift trái/phải nhẹ hoặc random hướng rơi cho star |
| Trung bình | Scene transition chưa "smooth" | Load scene trực tiếp | Có thể thêm fade panel nếu còn thời gian |
| Trung bình | Code documentation còn ít | README tốt, nhưng comment code chưa nhiều | Thêm comment ngắn ở overheat, lives, shield, scoring, scene transition |
| Trung bình | Chưa test full Play Mode/build | Checklist test vẫn chưa hoàn tất | Test Main Menu, Gameplay, End Game, shield, lives, Quit trong `.exe` |

### Kết luận đối chiếu nhanh

Project hiện đã đáp ứng phần lớn yêu cầu bắt buộc của đề. Điểm lệch đáng chú ý còn lại là **game không kết thúc ngay ở lần va chạm đầu vì đã nâng cấp thành hệ 3 mạng**. Luật va asteroid đã được chỉnh để trừ `500` điểm, thấp nhất là `0`, nhằm khớp yêu cầu trừ điểm của đề.

## Bảng tổng quan tiến độ

| Hạng mục | Trạng thái | Ghi chú |
| --- | --- | --- |
| Main Menu, Instructions và Settings | Đã triển khai | Play, hướng dẫn, settings chọn `WASD`/mũi tên và chuyển scene đã được nối |
| Player movement và shooting | Đã triển khai | Chế độ di chuyển chọn trong Settings, bắn bằng Space và chuột trái |
| Overheat và Heat UI | Đã triển khai | Heat, cooling thường/overheat và khóa bắn đã có |
| Asteroid và tăng độ khó | Đã triển khai | Spawn từ cạnh trên, random hướng rơi, spawn nhanh dần mỗi 30 giây |
| Star và scoring | Đã triển khai | Nhặt star `+100`, phá asteroid `+10` |
| Score, timer và End Game result | Đã triển khai | Gameplay có animated score counter; Gameplay và End Game đều có score/time |
| Scene transitions và Quit | Đã triển khai | Ba scene có trong Build Settings, Quit đã nối event |
| Toàn bộ Gameplay trong Play Mode | Chờ kiểm thử | Chưa xác nhận full flow trực tiếp trong Unity Editor |
| Hệ thống 3 mạng | Chờ kiểm thử | Logic 3 mạng đã được nối với asteroid collision |
| Life icon | Chờ kiểm thử | Đã thêm 3 icon `playerLife1_blue` dưới Score |
| Miễn nhiễm sau va chạm | Chờ kiểm thử | Đã triển khai `1.5s` và player nhấp nháy |
| Shield đồng/bạc/vàng | Chờ kiểm thử | Drop cùng luồng star, visual tĩnh đã canh thủ công, chặn một lần sát thương |
| Trừ điểm khi va asteroid | Đã triển khai | Không có shield thì va asteroid trừ `500` điểm, thấp nhất là `0` |
| Game over ngay khi chạm asteroid | Sai khác có chủ đích | Project dùng hệ 3 mạng thay vì game over ở hit đầu |
| Star scattered/random movement | Cần cải thiện | Hiện star spawn random theo X và rơi thẳng xuống |
| Smooth scene transition | Cần cải thiện | Hiện dùng `SceneManager.LoadScene()` trực tiếp, chưa có fade |
| Code documentation | Cần cải thiện | README/progress đã rõ, comment trong code còn ít |
| Gameplay/UI SFX | Chưa triển khai | Audio asset đã có nhưng chưa được nối vào gameplay/UI |
| High Score bằng PlayerPrefs | Chưa triển khai | Chưa lưu và hiển thị kỷ lục |
| Windows build và test Quit | Chờ kiểm thử | `Application.Quit()` cần được test trong bản build |
| Screenshot cho README | Chưa triển khai | Chưa bổ sung ảnh 3 scene vào tài liệu |

## Yêu cầu của Lab

### 1. Scene Main Menu

- [x] Tạo scene `MainMenu`.
- [x] Thêm nút Play.
- [x] Thêm nút Instructions.
- [x] Thêm nút Settings.
- [x] Hiển thị panel hướng dẫn chơi.
- [x] Hiển thị panel Settings để chọn chế độ điều khiển.
- [x] Settings UI nằm trực tiếp trong Hierarchy của scene `MainMenu`.
- [x] Đóng panel hướng dẫn chơi.
- [x] Đóng panel Settings.
- [x] Lưu lựa chọn `WASD` hoặc phím mũi tên bằng `PlayerPrefs`.
- [x] Kết nối nút Play để chuyển sang scene `Gameplay`.

Bằng chứng:
- Scene: `Assets/Scenes/MainMenu.unity`
- Script: `Assets/Scripts/MenuManager.cs`
- Build Settings đã có `Assets/Scenes/MainMenu.unity`

Trạng thái: **Đã hoàn thành**

### 2. Scene Gameplay

- [x] Tạo scene `Gameplay`.
- [x] Thêm tàu vũ trụ của người chơi.
- [x] Thêm bộ sinh asteroid.
- [x] Thêm bộ sinh star.
- [x] Thêm UI hiển thị điểm.
- [x] Thêm UI hiển thị thời gian chơi.
- [x] Thêm background và tài nguyên hình ảnh 2D.

Bằng chứng:
- Scene: `Assets/Scenes/Gameplay.unity`
- Trong scene đã có Player, `GameManager`, `AsteroidSpawner`, `StarSpawner`, `ScoreText`, và `TimerText`.

Trạng thái: **Đã triển khai, chờ Play Mode test**

### 3. Điều khiển người chơi

- [x] Tàu có thể di chuyển 4 hướng.
- [x] Tàu bị giới hạn trong màn hình camera.
- [x] Tàu có thể bắn laser.
- [x] Hỗ trợ phím mũi tên đúng theo yêu cầu Lab.
- [x] Có thể chọn chế độ điều khiển `WASD` hoặc phím mũi tên ở Main Menu.
- [x] Có hệ thống overheat khi bắn liên tục.
- [x] Hiển thị nhiệt vũ khí ở giữa phía trên màn hình.

Hiện tại:
- Di chuyển dùng chế độ đã chọn trong Main Menu: `WASD` hoặc phím mũi tên.
- Bắn bằng `Space` hoặc chuột trái.
- Mỗi phát bắn tăng `5` heat; đạt `100` thì khóa bắn.
- Khi chưa overheat, heat giảm `15` mỗi giây, kể cả lúc đang bắn.
- Khi đã overheat, heat giảm `25` mỗi giây; vũ khí chỉ hoạt động lại khi heat giảm xuống `0`.

Bằng chứng:
- Script: `Assets/Scripts/PlayerController.cs`

Trạng thái: **Đã hoàn thành**

### 4. Asteroid

- [x] Tạo prefab asteroid.
- [x] Sinh asteroid trong gameplay.
- [x] Asteroid di chuyển trong scene.
- [x] Asteroid có xoay.
- [x] Xóa asteroid khi ra khỏi màn hình.
- [x] Kích hoạt xử lý mất mạng/game over khi asteroid va chạm với player.
- [x] Cải thiện hướng di chuyển để asteroid ngẫu nhiên hơn.
- [x] Tăng độ khó: mỗi 30 giây asteroid spawn nhanh hơn.

Hiện tại:
- Vị trí sinh asteroid là ngẫu nhiên.
- Tốc độ và tốc độ xoay là ngẫu nhiên.
- `spawnRate` bắt đầu từ `1.5s`, mỗi `30s` giảm `0.2s`, thấp nhất là `0.5s`.
- Asteroid sinh từ cạnh trên màn hình, random vị trí X.
- Hướng rơi được random để asteroid bay xuống và lệch trái/phải.

Bằng chứng:
- Prefab: `Assets/Prefabs/Asteroid.prefab`
- Scripts:
  - `Assets/Scripts/AsteroidSpawner.cs`
  - `Assets/Scripts/AsteroidMove.cs`

Trạng thái: **Đã hoàn thành**

### 5. Star

- [x] Tạo prefab star.
- [x] Sinh star trong gameplay.
- [x] Star rơi từ trên xuống.
- [x] Xóa star khi ra khỏi màn hình.
- [x] Cộng điểm khi player nhặt star.

Điểm hiện tại:
- Nhặt star được `+100` điểm.

Bằng chứng:
- Prefab: `Assets/Prefabs/Star.prefab`
- Scripts:
  - `Assets/Scripts/StarSpawner.cs`
  - `Assets/Scripts/Star.cs`

Trạng thái: **Đã hoàn thành**

### 6. Shield item

- [x] Tạo prefab shield pickup.
- [x] Shield có thể rơi xuống giống star.
- [x] Mỗi lần spawn item có `15%` xác suất thay star bằng shield.
- [x] Nếu đã spawn 5 star liên tiếp, item tiếp theo chắc chắn là shield.
- [x] Shield đồng chiếm `60%` số shield drop và tồn tại `5s`.
- [x] Shield bạc chiếm `30%` số shield drop và tồn tại `10s`.
- [x] Shield vàng chiếm `10%` số shield drop và tồn tại `15s`.
- [x] Shield chặn một lần va chạm asteroid rồi biến mất.
- [x] Shield hết thời gian sẽ tự tắt.
- [x] Nhặt shield mới sẽ thay shield hiện tại và làm mới thời gian.
- [x] Shield visual tĩnh đã được canh thủ công quanh player.
- [ ] Xác nhận ba tier shield bằng Play Mode test.

Bằng chứng:
- Prefab: `Assets/Prefabs/Shield.prefab`
- Scripts:
  - `Assets/Scripts/ShieldPickup.cs`
  - `Assets/Scripts/StarSpawner.cs`
  - `Assets/Scripts/PlayerController.cs`

Trạng thái: **Đã triển khai, chờ Play Mode test**

### 7. Laser và bắn asteroid

- [x] Tạo prefab laser.
- [x] Sinh laser tại vị trí bắn của player.
- [x] Laser bay lên trên.
- [x] Xóa laser khi ra khỏi màn hình.
- [x] Laser phá asteroid khi va chạm.
- [x] Cộng điểm khi phá asteroid.

Điểm hiện tại:
- Phá asteroid được `+10` điểm.

Bằng chứng:
- Prefab: `Assets/Prefabs/Laser.prefab`
- Script: `Assets/Scripts/Laser.cs`

Trạng thái: **Đã hoàn thành**

### 8. Va chạm và tính điểm

- [x] Player nhặt star và được cộng điểm.
- [x] Laser bắn trúng asteroid và được cộng điểm.
- [x] Player va asteroid khi không có shield sẽ bị trừ `500` điểm, thấp nhất là `0`.
- [x] Score UI trong Gameplay tăng/giảm dần tới giá trị mới bằng animated score counter.
- [x] Player mất mạng khi chạm asteroid và chuyển sang End Game ở lần va chạm thứ ba.
- [x] Điểm cuối được lưu và hiển thị sau khi game over.
- [x] Thời gian chơi được hiển thị trong Gameplay.
- [x] Thời gian chơi cuối cùng được hiển thị cùng hàng với score ở End Game.
- [x] Thay luật game over ngay bằng hệ thống 3 mạng ở mức code và scene wiring.
- [ ] Xác nhận hệ thống 3 mạng bằng Play Mode test.

Ghi chú:
- Đề Lab có nhắc "collisions with asteroids deduct points", nhưng phần Game Flow lại ghi "game ends if the spaceship collides with an asteroid".
- Player hiện có 3 mạng và bị trừ `500` điểm khi va asteroid mà không có shield.
- Điểm thấp nhất là `0`, không bị âm.
- Score thật vẫn được lưu ngay lập tức; chỉ phần hiển thị `ScoreText` chạy animation.
- Nếu đang có shield, shield chặn va chạm một lần nên player không bị trừ điểm và không mất mạng.
- Sau khi mất một mạng, player nhấp nháy và miễn nhiễm trong `1.5s`.
- Lần va chạm thứ ba mới chuyển sang End Game.

Bằng chứng:
- `Assets/Scripts/GameManager.cs`
- `Assets/Scripts/Star.cs`
- `Assets/Scripts/Laser.cs`
- `Assets/Scripts/AsteroidMove.cs`

Trạng thái: **Đã chạy được, nên giải thích rõ luật trong phần thuyết trình/nộp bài**

### 9. Scene End Game

- [x] Tạo scene `EndGame`.
- [x] Hiển thị điểm cuối.
- [x] Thêm nút Play Again.
- [x] Thêm nút Main Menu.
- [x] Có hàm `QuitGame()` trong `EndGameManager.cs`.
- [x] Thêm và kết nối nút Quit trong scene.

Bằng chứng:
- Scene: `Assets/Scenes/EndGame.unity`
- Scripts:
  - `Assets/Scripts/EndGameManager.cs`
  - `Assets/Scripts/EndGameUI.cs`

Ghi chú:
- `EndGameManager.cs` đã có hàm `QuitGame()`.
- Trong `EndGame.unity` đã có `QuitButton` và On Click gọi `EndGameManager.QuitGame`.

Trạng thái: **Đã hoàn thành**

### 10. Chuyển scene

- [x] Main Menu sang Gameplay.
- [x] Gameplay sang End Game.
- [x] End Game sang Gameplay.
- [x] End Game sang Main Menu.
- [x] Các scene đã được thêm vào Build Settings.

Bằng chứng:
- `ProjectSettings/EditorBuildSettings.asset`
- `Assets/Scripts/MenuManager.cs`
- `Assets/Scripts/GameManager.cs`
- `Assets/Scripts/EndGameManager.cs`

Trạng thái: **Đã hoàn thành**

### 11. Tài liệu

- [x] Thêm file README cho project.
- [x] Giải thích ý tưởng game.
- [x] Giải thích cách điều khiển.
- [x] Giải thích các scene.
- [x] Giải thích cách tính điểm và xử lý va chạm.
- [x] Liệt kê các script chính và nhiệm vụ của từng script.
- [ ] Thêm ảnh chụp màn hình nếu giảng viên yêu cầu.

Bằng chứng:
- `README.md`

Trạng thái: **Gần hoàn thành**

## Đối chiếu nhanh với đề Lab 1

| Yêu cầu trong đề | Trạng thái hiện tại | Ghi chú |
| --- | --- | --- |
| Tạo game 2D tên `Space Explorer` | Đã hoàn thành | Project, README và gameplay đều theo đúng tên/chủ đề |
| Main Menu có Play và Instructions | Đã hoàn thành | Có scene `MainMenu`, nút Play và panel hướng dẫn |
| Player di chuyển bằng phím mũi tên | Đã hoàn thành | Có chế độ phím mũi tên mặc định; Settings có thêm lựa chọn `WASD` |
| Player bắn laser | Đã hoàn thành | Bắn bằng `Space` hoặc chuột trái, có overheat mở rộng |
| Asteroid xuất hiện và di chuyển ngẫu nhiên | Đã hoàn thành | Asteroid spawn từ cạnh trên, hướng rơi/tốc độ/xoay ngẫu nhiên |
| Star xuất hiện và có thể nhặt | Đã hoàn thành | Star rơi xuống, nhặt được `+100` điểm |
| Collision và scoring | Đã triển khai, chờ test cuối | Star cộng điểm, laser phá asteroid cộng điểm, player va asteroid bị trừ `500` điểm và mất mạng nếu không có shield |
| Chuyển scene Main Menu, Gameplay, End Game | Đã hoàn thành | Các scene đã nối bằng `SceneManager` |
| End Game hiển thị score | Đã hoàn thành | Hiển thị score và mở rộng thêm thời gian chơi |
| Quit game | Đã triển khai, chờ build test | Editor không thoát thật, cần test trong bản `.exe` |
| Documentation | Gần hoàn thành | README và file tiến độ đã có, còn thiếu screenshot nếu cần nộp |

Nhìn theo đề gốc, project hiện đã đạt gần đủ phần bắt buộc. Các tính năng overheat, timer, 3 mạng, shield tier và tăng độ khó là phần mở rộng để tăng điểm sáng tạo.

## Ước lượng điểm theo rubric

### Functionality - 50 điểm

Ước lượng hiện tại: **47-49 / 50**

Đã có:
- Player movement
- Shooting
- Weapon overheat and cooling system
- Asteroid spawning and movement
- Asteroid spawn rate increases every 30 seconds
- Star spawning and collection
- Collision handling
- Asteroid hit score penalty, minimum score `0`
- Animated score counter in Gameplay UI
- Score display
- Gameplay timer display
- End Game displays final score and final time
- Scene transitions
- End game score display

Cần làm thêm:
- Test toàn bộ flow trong Unity Editor.
- Build thử bản Windows để xác nhận Quit và scene flow ngoài Editor.

### Creativity - 20 điểm

Ước lượng hiện tại: **15-18 / 20**

Đã có:
- Có chủ đề không gian rõ ràng.
- Có sprite tàu, asteroid, laser, star, UI và background.
- Có tăng độ khó theo thời gian thông qua asteroid spawn nhanh dần.
- Có phản hồi màu cho heat: trắng, vàng khi nóng và đỏ khi overheat.

Có thể cải thiện:
- Thêm gameplay/UI SFX bằng audio asset đã có.
- Thêm hiệu ứng nổ hoặc particle.
- Thêm nhiều kích thước hoặc kiểu di chuyển asteroid khác nhau.
- Thêm high score để người chơi có mục tiêu chơi lại.

### Documentation - 30 điểm

Ước lượng hiện tại: **24-28 / 30**

Đã có:
- `README.md` giải thích game, scene, controls, scoring và scripts.
- File tiến độ này giúp đối chiếu yêu cầu Lab.

Có thể cải thiện:
- Thêm ảnh chụp màn hình.
- Thêm link video demo nếu được yêu cầu.
- Thêm vài comment trong code ở những phần logic quan trọng.

## Backlog đã thống nhất

- [ ] Thêm SFX bắn laser, nhặt star, phá asteroid, player trúng đòn, game over và button click.
- [ ] Lưu High Score bằng `PlayerPrefs`.
- [ ] Hiển thị `High Score` hoặc `NEW HIGH SCORE` trong End Game.
- [ ] Nếu còn thời gian, thêm hiệu ứng nổ nhỏ khi laser phá asteroid.

## Kiểm thử và hoàn thiện

- [ ] Test Main Menu, panel Instructions và panel Settings.
- [ ] Test chọn `WASD`, vào Gameplay chỉ di chuyển bằng WASD.
- [ ] Test chọn phím mũi tên, vào Gameplay chỉ di chuyển bằng phím mũi tên.
- [ ] Test shooting, overheat, cooling và mở khóa tại `0%`.
- [ ] Test asteroid spawn từ cạnh trên, rơi ngẫu nhiên và tăng tốc mỗi 30 giây.
- [ ] Test ba lần va chạm: mất từng life icon và lần thứ ba mới Game Over.
- [ ] Test va asteroid trừ `500` điểm, thấp nhất là `0`.
- [ ] Test score UI tăng/giảm dần tới đúng giá trị thật.
- [ ] Test có shield thì va asteroid không trừ điểm và không mất life icon.
- [ ] Test player không mất thêm mạng trong `1.5s` miễn nhiễm.
- [ ] Test shield đồng `5s`, bạc `10s`, vàng `15s`.
- [ ] Test shield chặn đúng một va chạm và không làm mất life icon.
- [ ] Test shield tự hết hạn và nhặt shield mới làm mới thời gian.
- [ ] Test star, scoring, timer và kết quả End Game.
- [ ] Test Play Again, Main Menu và Quit.
- [ ] Build Windows `.exe` và kiểm tra `Application.Quit()`.
- [ ] Chụp ảnh Main Menu, Gameplay và End Game để thêm vào README.

## Thứ tự thực hiện tiếp theo

1. Playtest lives, life icon và miễn nhiễm; sau đó triển khai audio và high score.
2. Test toàn bộ gameplay trong Unity Play Mode và sửa lỗi phát hiện được.
3. Build Windows, test Quit và hoàn thiện README bằng screenshot.

## Kết luận tiến độ

Project đã hoàn thành phần lớn yêu cầu Lab ở mức code và scene wiring. Gameplay core hiện **đã triển khai nhưng chưa được xác nhận bằng full Play Mode test**. Hệ thống 3 mạng, life icon, miễn nhiễm `1.5s` và shield đồng/bạc/vàng đã được triển khai, đang chờ kiểm thử. Gameplay/UI SFX và High Score vẫn chưa triển khai. Sau đó cần build Windows và hoàn thiện tài liệu nộp bài.
