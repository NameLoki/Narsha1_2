using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {
	public ParticleSystem[] ptc;

	void Start() {
		ptc[0].Play();
	}

	
	public void PlayParitcle(int i, int j) {	// i : 1.찌르기 2.베기 3.패링  j : 1.perfect 2.good 3.bad
		int num;
		if (j == 3) {	// bad는 통일
			num = 7;
		} else {
			num = (i - 1) * 2 + j;
		}


		if (ptc[num]) {							// 파티클이 있고
			if (ptc[num].isPlaying) {	// 파티클이 재생중이면 삭제 후 다시 재생
				ptc[num].Clear();
				ptc[num].Play();
			} else {						// 재생중이 아니라면 재생
				ptc[num].Play();
			}	
		}
	}
}
