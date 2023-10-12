mergeInto(LibraryManager.library, {

  	 RateGame: function () {
      
       ysdk.feedback.canReview()
            .then(({ value, reason }) => {
                if (value) {
                    ysdk.feedback.requestReview()
                        .then(({ feedbackSent }) => {
                        
                            console.log(feedbackSent);
                        })
                } else {
                     console.log(reason)
                }
            })  
      },
      
     CollectPlayerData: function () {
      
          player.getData().then(_date =>
          {
            const myJSON = JSON.stringify(_date);
            myGameInstance.SendMessage("Progress","SetPlayerInfo", myJSON);
            console.log('MY DEBUG CollectPlayerData');
          });
          
      },
      
      SavePlayerData: function (date) {
     
        var dateString = UTF8ToString(date);
        var myobj = JSON.parse(dateString);
        player.setData(myobj);
        console.log('MY DEBUG SavePlayerData');
       },
      
    ShowAdv: function ()
    {
    
        myGameInstance.SendMessage("Menu","SoundOnOff");
          ysdk.adv.showFullscreenAdv({
              callbacks: {
                  onClose: function(wasShown) {
                     myGameInstance.SendMessage("Menu",SoundOnOff);
                       console.log('MY DEBUG Off Sound');
                  },
                  onError: function(error) {
                    // some action on error
                  }
              }
          })
    },
     ShowRewardedAdv: function ()
        {
            ysdk.adv.showRewardedVideo({
                callbacks: {
                    onOpen: () => {
                      console.log('MY DEBUG Video ad open.');
                    },
                    onRewarded: () => {
                        myGameInstance.SendMessage("Yandex","AddReward");
                      console.log('MY DEBUG Rewarded!');
                    },
                    onClose: () => {
                      console.log('MY DEBUG Video ad closed.');
                    }, 
                    onError: (e) => {
                      console.log('MY DEBUG Error while open video ad:', e);
                    }
                }
            })
            
        },
  });