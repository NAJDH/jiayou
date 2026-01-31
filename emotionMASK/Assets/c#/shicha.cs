using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    [Header("核心配置")]
    public Camera mainCamera; // 拖拽你的主相机
    [SerializeField] private float parallaxEffect = 0.5f; // 0=跟随相机，1=固定不动
    [SerializeField] private bool isHorizontalScroll = true; // 横向滚动（你的场景）

    private SpriteRenderer spriteRenderer;
    private float backgroundWidth; // 背景图片实际宽度（带缩放）
    private Vector3 initialLocalPos; // 背景初始本地位置
    private Vector3 initialCameraPos; // 手动记录相机初始位置（修复报错的核心）

    void Awake()
    {
        // 初始化关键组件和参数
        if (mainCamera == null) mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (spriteRenderer == null)
        {
            Debug.LogError("背景物体缺少SpriteRenderer组件！", this);
            enabled = false;
            return;
        }

        // 记录相机初始位置（修复CS1061报错）
        initialCameraPos = mainCamera.transform.position;
        
        // 计算带缩放的实际宽度
        backgroundWidth = spriteRenderer.bounds.size.x;
        initialLocalPos = transform.localPosition; // 用本地坐标更稳定
    }

    void LateUpdate() // 用LateUpdate确保相机移动后再更新背景
    {
        if (!isHorizontalScroll || mainCamera == null) return;

        // 1. 计算视差偏移（修复：使用手动记录的初始相机位置）
        float cameraDeltaX = mainCamera.transform.position.x - initialCameraPos.x;
        float parallaxDelta = cameraDeltaX * parallaxEffect;

        // 2. 更新背景位置（仅偏移，不修改基准值）
        transform.localPosition = new Vector3(
            initialLocalPos.x + parallaxDelta,
            transform.localPosition.y,
            transform.localPosition.z
        );

        // 3. 无限滚动判断（用背景边缘与相机的相对位置）
        float cameraWorldX = mainCamera.transform.position.x;
        float backgroundLeftEdge = transform.position.x - backgroundWidth / 2;
        float backgroundRightEdge = transform.position.x + backgroundWidth / 2;

        // 向右滚动：相机超过背景右边缘 → 背景右移一个宽度
        if (cameraWorldX > backgroundRightEdge)
        {
            transform.Translate(Vector3.right * backgroundWidth);
            initialLocalPos = transform.localPosition; // 更新基准位置
            initialCameraPos = mainCamera.transform.position; // 重置相机初始位置，避免累积偏移
        }
        // 向左滚动：相机超过背景左边缘 → 背景左移一个宽度
        else if (cameraWorldX < backgroundLeftEdge)
        {
            transform.Translate(Vector3.left * backgroundWidth);
            initialLocalPos = transform.localPosition; // 更新基准位置
            initialCameraPos = mainCamera.transform.position; // 重置相机初始位置
        }
    }

    // 可选：Gizmos可视化背景边界，方便调试
    void OnDrawGizmos()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Gizmos.color = Color.red;
            Vector3 center = spriteRenderer.bounds.center;
            Vector3 size = spriteRenderer.bounds.size;
            Gizmos.DrawWireCube(center, size); // 绘制背景边界框
        }
    }
}