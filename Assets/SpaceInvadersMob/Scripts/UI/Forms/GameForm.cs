using SpaceInvadersMob.Extensions.UI;
using TMPro;
using UnityEngine;

namespace SpaceInvadersMob.UI
{
    public class GameForm : BaseForm<GameForm>
    {
        [SerializeField] private ButtonExt _bntMenu;
        [SerializeField] private TextMeshProUGUI _redEnemyCount;
        [SerializeField] private TextMeshProUGUI _baseEnemyCount;


        protected override void setup()
        {
            _baseEnemyCount.text = _redEnemyCount.text = "x0";
        }
    }
}
