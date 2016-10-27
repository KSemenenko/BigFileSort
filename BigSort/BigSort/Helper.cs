﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BigSort
{
    class Helper
    {
        static Helper()
        {
            WordsDictionary = new Dictionary<int, string>();
            var wordList = new List<string>(@"
1.jump 2.gabby 3.suggestion 4.collar 5.general 6.beautiful 7.partner 8.unused 9.shaky 10.jar 11.spotless 12.comparison 13.third 14.spooky 15.tight 16.incompetent 17.mute 18.rule 19.receipt 20.blush 21.stamp 22.handsomely 23.finger 24.gleaming 25.silent 26.voice 27.polite 28.value 29.offend 30.inexpensive 31.thank 32.trains 33.wash 34.elfin 35.ajar 36.boorish 37.pass 38.ice 39.winter 40.rural 41.drain 42.exultant 43.lumpy 44.hot 45.women 46.lyrical 47.gold 48.start 49.empty 50.guitar 51.oranges 52.license 53.screw 54.design 55.stove 56.fall 57.slope 58.itch 59.wide-eyed 60.roasted 61.agreeable 62.impartial 63.borrow 64.exuberant 65.copy 66.level 67.right 68.strap 69.spring 70.enjoy 71.discussion 72.zippy 73.sidewalk 74.annoy 75.wax 76.earthquake 77.cheer 78.tempt 79.bird 80.bit 81.change 82.form 83.retire 84.six 85.cut 86.harbor 87.rainy 88.noise 89.lumber 90.sharp 91.maddening 92.wash 93.pen 94.alike 95.dynamic 96.ready 97.cooperative 98.expansion 99.liquid 100.science 101.rose 102.morning 103.inconclusive 104.awake 105.knowing 106.sugar 107.sticks 108.wealthy 109.eager 110.knowledge 111.irate 112.jealous 113.arrest 114.paper 115.development 116.vase 117.meal 118.disturbed 119.buzz 120.impossible 121.recognise 122.ceaseless 123.hanging 124.massive 125.berserk 126.muddle 127.screw 128.try 129.dysfunctional 130.week 131.mine 132.hissing 133.measly 134.produce 135.bomb 136.broken 137.choke 138.endurable 139.future 140.pop 141.calculating 142.bead 143.tall 144.afternoon 145.agonizing 146.admit 147.thick 148.hushed 149.thoughtful 150.word 151.pot 152.dear 153.guard 154.cure 155.underwear 156.rinse 157.please 158.increase 159.bell 160.tiny 161.cute 162.capable 163.dogs 164.round 165.decide 166.way 167.allow 168.stick 169.run 170.sophisticated 171.paste 172.amount 173.profit 174.premium 175.flight 176.spray 177.pink 178.absorbed 179.grate 180.juvenile 181.tacky 182.bubble 183.dirt 184.busy 185.absent 186.imported 187.mind 188.few 189.rebel 190.carry 191.apparel 192.question 193.cactus 194.dolls 195.smiling 196.fearless 197.song 198.occur 199.mend 200.lunch 201.absorbing 202.laugh 203.muscle 204.deadpan 205.smile 206.belong 207.secretary 208.crazy 209.dust 210.snobbish 211.plug 212.hill 213.calm 214.vacation 215.taboo 216.religion 217.neck 218.sink 219.tail 220.scream 221.consist 222.zoo 223.motionless 224.exciting 225.illustrious 226.step 227.spade 228.far-flung 229.momentous 230.extra-small 231.punish 232.common 233.sable 234.soap 235.cough 236.jewel 237.manage 238.sense 239.range 240.balance 241.puffy 242.snails 243.boundary 244.birds 245.aunt 246.crow 247.plant 248.glistening 249.sheet 250.territory 251.jelly 252.fresh 253.eight 254.desire 255.shut 256.inform 257.action 258.wrist 259.baseball 260.lucky 261.hum 262.man 263.charming 264.squirrel 265.songs 266.copper 267.concern 268.wary 269.handsome 270.gentle 271.society 272.bath 273.park 274.noxious 275.lamentable 276.incredible 277.neat 278.gaze 279.nifty 280.bear 281.weigh 282.country 283.warm 284.pocket 285.twig 286.throne 287.untidy 288.board 289.great 290.breakable 291.hate 292.craven 293.cynical 294.afterthought 295.crib 296.abhorrent 297.harsh 298.tasty 299.last 300.abiding 301.trip 302.irritating 303.spiteful 304.lock 305.increase 306.rigid 307.literate 308.lethal 309.measure 310.psychedelic 311.cracker 312.kaput 313.three 314.terrific 315.match 316.precious 317.majestic 318.cast 319.longing 320.good 321.furniture 322.daughter 323.queue 324.waiting 325.fabulous 326.warm 327.itchy 328.coat 329.root 330.reaction 331.sparkling 332.small 333.dream 334.road 335.back 336.tranquil 337.attempt 338.scarf 339.love 340.string 341.abandoned 342.call 343.part 344.pan 345.introduce 346.impress 347.rub 348.rhetorical 349.guiltless 350.thing 351.symptomatic 352.number 353.dime 354.highfalutin 355.divide 356.tooth 357.match 358.permit 359.recess 360.happen 361.unfasten 362.plant 363.boil 364.rifle 365.soda 366.ordinary 367.bait 368.yellow 369.appliance 370.equal 371.plot 372.shoes 373.bubble 374.rock 375.frame 376.obtain 377.swift 378.strong 379.stingy 380.eye 381.earsplitting 382.memorise 383.even 384.smelly 385.deliver 386.flowery 387.dispensable 388.chop 389.ski 390.earn 391.fowl 392.sincere 393.yummy 394.eminent 395.passenger 396.ratty 397.reply 398.bump 399.pigs 400.dashing 401.wound 402.flashy 403.spiders 404.bee 405.guide 406.vessel 407.flag 408.sprout 409.delight 410.eggnog 411.star 412.fang 413.branch 414.invention 415.wine 416.obscene 417.unaccountable 418.white 419.soak 420.reading 421.shrug 422.noisy 423.crack 424.tidy 425.unwritten 426.giddy 427.hop 428.intend 429.launch 430.heady 431.tangible 432.profuse 433.turn 434.roof 435.mass 436.rabbit 437.chilly 438.tearful 439.picture 440.protest 441.vengeful 442.violent 443.dog 444.disappear 445.decision 446.excite 447.plastic 448.inquisitive 449.interesting 450.petite 451.office 452.chickens 453.stiff 454.need 455.basin 456.vagabond 457.change 458.naughty 459.macabre 460.laugh 461.thundering 462.numerous 463.holistic 464.fortunate 465.fence 466.narrow 467.boat 468.whistle 469.actor 470.skinny 471.squalid 472.acceptable 473.slip 474.reach 475.puny 476.transport 477.excited 478.innate 479.quixotic 480.big 481.advise 482.madly 483.cloudy 484.cry 485.kitty 486.thumb 487.subtract 488.shop 489.mixed 490.men 491.summer 492.locket 493.aboriginal 494.enormous 495.fanatical 496.size 497.fool 498.whirl 499.mist 500.nasty 501.follow 502.funny 503.heavenly 504.risk 505.camp 506.knock 507.rescue 508.scold 509.disagree 510.stormy 511.ruthless 512.bounce 513.memory 514.scary 515.cannon 516.actually 517.adamant 518.evasive 519.spell 520.worm 521.touch 522.alleged 523.chance 524.laborer 525.face 526.tent 527.twist 528.mountainous 529.sail 530.hard-to-find 531.flat 532.ossified 533.capricious 534.shiver 535.team 536.mature 537.bake 538.zonked 539.bikes 540.wool 541.suggest 542.decorate 543.keen 544.sordid 545.super 546.rotten 547.describe 548.boot 549.sound 550.greasy 551.fearful 552.cold 553.encourage 554.nutty 555.aquatic 556.talented 557.sheep 558.continue 559.clammy 560.driving 561.town 562.squash 563.stocking 564.drag 565.fear 566.rat 567.sturdy 568.communicate 569.worry 570.voracious 571.pin 572.scandalous 573.develop 574.thread 575.flagrant 576.overrated 577.baby 578.wooden 579.brown 580.jog 581.separate 582.abrupt 583.grab 584.learned 585.oven 586.puzzling 587.arithmetic 588.used 589.toy 590.beginner 591.evanescent 592.pricey 593.living 594.flavor 595.husky 596.coil 597.magnificent 598.skirt 599.shivering 600.hysterical 601.uncovered 602.afraid 603.haircut 604.tasteless 605.left 606.instrument 607.expert 608.grouchy 609.shame 610.unsightly 611.film 612.gate 613.horn 614.loving 615.end 616.resonant 617.mask 618.hungry 619.representative 620.abounding 621.calendar 622.sneaky 623.spoon 624.repeat 625.trashy 626.daffy 627.fail 628.upset 629.lick 630.fasten 631.invite 632.air 633.substance 634.girls 635.add 636.horrible 637.bone 638.deer 639.donkey 640.boring 641.punishment 642.brake 643.spectacular 644.chin 645.avoid 646.day 647.wander 648.pancake 649.release 650.parched 651.ritzy 652.education 653.repulsive 654.soup 655.unequaled 656.frog 657.intelligent 658.elbow 659.fade 660.replace 661.person 662.cream 663.nervous 664.rambunctious 665.cup 666.prevent 667.unwieldy 668.tramp 669.turkey 670.mundane 671.pull 672.shaggy 673.political 674.grey 675.phone 676.brainy 677.vast 678.quiet 679.friendly 680.descriptive 681.bury 682.things 683.trace 684.ludicrous 685.lackadaisical 686.hall 687.label 688.extend 689.groan 690.imagine 691.barbarous 692.opposite 693.aggressive 694.amused 695.rapid 696.grease 697.quick 698.burly 699.plane 700.entertaining 701.moan 702.lavish 703.smooth 704.crown 705.crooked 706.harmonious 707.enchanting 708.popcorn 709.jobless 710.creepy 711.zany 712.teeny-tiny 713.hour 714.material 715.easy 716.blot 717.unsuitable 718.sudden 719.delirious 720.ubiquitous 721.bike 722.ambiguous 723.trite 724.drop 725.advice 726.glue 727.test 728.unite 729.market 730.drawer 731.loud 732.breezy 733.scrape 734.attack 735.compare 736.party 737.wire 738.habitual 739.print 740.cent 741.behave 742.bashful 743.debt 744.program 745.black-and-white 746.knife 747.influence 748.yielding 749.defeated 750.fetch 751.uneven 752.battle 753.little 754.flock 755.stage 756.experience 757.voiceless 758.bustling 759.mint 760.hellish 761.various 762.moldy 763.long-term 764.guide 765.assorted 766.redundant 767.well-to-do 768.blink 769.steep 770.earth 771.box 772.effect 773.brush 774.enthusiastic 775.badge 776.pleasure 777.dusty 778.weather 779.smell 780.shade 781.discover 782.nice 783.fragile 784.pig 785.respect 786.rare 787.note 788.miss 789.waste 790.maid 791.committee 792.bored 793.smash 794.touch 795.icicle 796.defective 797.dare 798.open 799.volleyball 800.skin 801.tough 802.bolt 803.adorable 804.daily 805.idiotic 806.enter 807.sisters 808.wall 809.disgusting 810.egg 811.berry 812.doctor 813.suit 814.drum 815.horses 816.slimy 817.blow 818.sneeze 819.uppity 820.tendency 821.suspend 822.embarrass 823.foot 824.piquant 825.pizzas 826.moon 827.dislike 828.enchanted 829.godly 830.smoggy 831.name 832.frequent 833.addition 834.fire 835.torpid 836.joke 837.hang 838.self 839.bang 840.act 841.gifted 842.tense 843.level 844.giants 845.curvy 846.land 847.callous 848.pet 849.careful 850.burst 851.ill-fated 852.unbiased 853.large 854.tray 855.boundless 856.creature 857.historical 858.fly 859.bitter 860.guarded 861.sore 862.innocent 863.vulgar 864.ear 865.knit 866.home 867.loss 868.zipper 869.part 870.pack 871.verse 872.condemned 873.repair 874.roomy 875.spoil 876.injure 877.wrap 878.melodic 879.bloody 880.stir 881.frantic 882.overflow 883.pretend 884.crabby 885.cover 886.stem 887.letter 888.rake 889.reproduce 890.rate 891.wrestle 892.nail 893.care 894.chess 895.utopian 896.snotty 897.claim 898.excellent 899.grateful 900.contain 901.story 902.collect 903.cool 904.flower 905.bed 906.condition 907.fix 908.damaged 909.abusive 910.deep 911.connection 912.dry 913.drab 914.unhealthy 915.salt 916.current 917.tense 918.fertile 919.damaging 920.complete 921.thirsty 922.gigantic 923.purpose 924.boiling 925.store 926.woebegone 927.slap 928.cats 929.pickle 930.tease 931.request 932.amusing 933.order 934.exotic 935.sofa 936.acidic 937.mom 938.imaginary 939.familiar 940.smile 941.sip 942.tumble 943.soft 944.irritate 945.smoke 946.engine 947.head 948.cluttered 949.alert 950.rest 951.dangerous 952.oceanic 953.blood 954.responsible 955.cuddly 956.gaping 957.muddled 958.walk 959.slip 960.wind 961.public 962.sponge 963.frightened 964.harass 965.table 966.wait 967.abject 968.flash 969.argue 970.jail 971.shelter 972.brave 973.exercise 974.trail 975.lopsided 976.chief 977.obsolete 978.questionable 979.scattered 980.existence 981.file 982.high-pitched 983.skip 984.cherry 985.giant 986.angry 987.monkey 988.icy 989.overjoyed 990.draconian 991.cows 992.ashamed 993.cat 994.tremendous 995.statuesque 996.suppose 997.brick 998.closed 999.correct 1000.hurried
".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

            foreach(var item in wordList)
            {
                try
                {
                    var element = item.Split('.');
                    WordsDictionary.Add(Convert.ToInt32(element[0]),element[1]);
                }
                catch(Exception ex)
                {
                    // skip this
                    Debug.WriteLine(ex.Message);
                }
            }

        }

        public static Dictionary<int, string> WordsDictionary;

        public static IEnumerable<string> GetRandomValue(int count)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            for(int i = 0; i < count; i++)
            {
                yield return string.Format("{0}.{1}", rnd.Next(0, WordsDictionary.Count * 2), WordsDictionary[rnd.Next(1, WordsDictionary.Count-1)]);
            }
        } 

    }
}