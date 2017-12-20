#!/usr/bin/python

import sys

class NBIAS_data_metric:
    def __init__(self, fields_str):
        self.parse(fields_str)

    def parse(self, str):
        self.full_bridge_id = str[:18]
        self.on_under = str[18:19]
        self.kind_hwy = str[19:20]
        self.levl_srvc = str[20:21]
        self.routenum = str[21:26]
        self.dirsuffix = str[26:27]
        self.district = str[27:29]
        self.county = str[29:32]
        self.placecode = str[32:37]
        self.featint = str[37:61]
        self.crit_feat = str[61:62]
        self.facility = str[62:80]
        self.location = str[80:105]
        self.vclrinv = str[105:109]
        self.kmpost = str[109:116]
        self.onbasenet = str[116:117]
        self.lrsinvrt = str[117:127]
        self.subrtnum = str[127:129]
        self.latitude = str[129:137]
        self.longitude = str[137:146]
        self.bypasslen = str[146:149]
        self.tollfac = str[149:150]
        self.custodian = str[150:152]
        self.owner = str[152:154]
        self.funcclass = str[154:156]
        self.yearbuilt = str[156:160]
        self.lanes_on = str[160:162]
        self.lanes_under = str[162:164]
        self.adttotal = str[164:170]
        self.adtyear = str[170:174]
        self.designload = str[174:175]
        self.aroadwidth = str[175:179]
        self.bridgemed = str[179:180]
        self.skew = str[180:182]
        self.strflared = str[182:183]
        self.railrating = str[183:184]
        self.transratin = str[184:185]
        self.arailratin = str [185:186]
        self.aendrating = str[186:187]
        self.histsign = str[187:188]
        self.navcntrol = str[188:189]
        self.navvc = str[189:193]
        self.navhc = str[193:198]
        self.oppostcl = str[198:199]
        self.servtypon = str[199:200]
        self.servtypund = str[200:201]
        self.material_main = str[201:202]
        self.design_main = str[202:204]
        self.material_appr = str[204:205]
        self.design_appr = str[205:207]
        self.mainspans = str[207:209]
        self.appspans = str[209:214]
        self.hclrinv = str[214:217]
        self.maxspan = str[217:222]
        self.length = str[222:228]
        self.lftcurbsw = str[228:231]
        self.rtcurbsw = str[231:234]
        self.roadwidth = str[234:238]
        self.deckwidth = str[238:242]
        self.vclrover = str[242:246]
        self.refvuc = str[246:247]
        self.vclrunder = str[247:251]
        self.refhuc = str[251:252]
        self.hclrurt = str[252:255]
        self.hclrult = str[255:258]
        self.dkrating = str[258:259]
        self.suprating = str[259:260]
        self.subrating = str[260:261]
        self.chanprot = str[261:262]
        self.culvrating = str[262:263]
        self.ortype = str[263:264]
        self.orload = str[264:267]
        self.irtype = str[267:268]
        self.irload = str[268:271]
        self.strrating = str[271:272]
        self.deckgeom = str[272:273]
        self.underclr = str[273:274]
        self.posting = str[274:275]
        self.wateradeq = str[275:276]
        self.appralign = str[276:277]
        self.propwork = str[277:279]
        self.workby = str[279:280]
        self.implen = str[280:286]
        self.inspdate = str[286:290]
        self.nbinspfreq = str[290:292]
        self.fraccrit = str[292:293]
        self.fcinspfreq = str[293:294]
        self.uwinspreq = str[294:295]
        self.uwinspfreq = str[295:296]
        self.osinspreq = str[296:298]
        self.osinspfreq = str[298:301]
        self.fclastinsp = str[301:305]
        self.uwlastinsp = str[305:309]
        self.oslastinsp = str[309:313]
        self.nbiimpcost = str[313:319]
        self.nbirwcost = str[319:325]
        self.nbitotcost = str[325:331]
        self.nbiyrcost = str[331:335]
        self.nstatecode = str[335:337]
        self.n_fhwa_reg = str[337:338]
        self.bb_pct = str[338:340]
        self.bb_brdgeid = str[340:355]
        self.defhwy = str[355:356]
        self.paralstruc = str[356:357]
        self.trafficdir = str[357:358]
        self.tempstruc = str[358:359]
        self.nhs_ind = str[359:360]
        self.fedlandhwy = str[360:361]
        self.yearrecon = str[361:365]
        self.dkstructyp = str[365:366]
        self.dksurftype = str[366:367]
        self.dkmembtype = str[367:368]
        self.dkprotect = str[368:369]
        self.truckpct = str[369:371]
        self.trucknet = str[371:372]
        self.pierprot = str[372:373]
        self.nbislen = str[373:374]
        self.scourcrit = str[374:375]
        self.adtfuture = str[375:381]
        self.adtfutyear = str[381:385]
        self.lftbrnavcl = str[385:389]
        self.reserved = str[389:426]
        self.nbi_rating = str[426:427]
        self.suff_prefx = str[427:428]
        self.suff_rate = str[428:]

    def toString(self):
        results = [self.on_under, self.kind_hwy, self.levl_srvc, self.routenum,
                   self.dirsuffix, self.district, self.county, self.placecode, self.featint, self.crit_feat,
                   self.facility, self.location, self.vclrinv, self.kmpost, self.onbasenet, self.lrsinvrt, self.subrtnum,
                   self.latitude, self.longitude, self.bypasslen, self.tollfac, self.custodian, self.owner, self.funcclass,
                   self.yearbuilt, self.lanes_on, self.lanes_under, self.adttotal, self.adtyear, self.designload,
                   self.aroadwidth, self.bridgemed, self.skew, self.strflared, self.railrating, self.transratin,
                   self.arailratin, self.aendrating, self.histsign, self.navcntrol, self.navvc, self.navhc, self.oppostcl,
                   self.servtypon, self.servtypund, self.material_main, self.design_main, self.material_appr,
                   self.design_appr, self.mainspans, self.appspans, self.hclrinv, self.maxspan, self.length, self.lftcurbsw,
                   self.rtcurbsw, self.roadwidth, self.deckwidth, self.vclrover, self.refvuc, self.vclrunder, self.refhuc,
                   self.hclrurt, self.hclrult, self.dkrating, self.suprating, self.subrating, self.chanprot, self.culvrating,
                   self.ortype, self.orload, self.irtype, self.irload, self.strrating, self.deckgeom, self.underclr,
                   self.posting, self.wateradeq, self.appralign, self.propwork, self.workby, self.implen, self.inspdate,
                   self.nbinspfreq, self.fraccrit, self.fcinspfreq, self.uwinspreq, self.uwinspfreq, self.osinspreq,
                   self.osinspfreq, self.fclastinsp, self.uwlastinsp, self.oslastinsp, self.nbiimpcost, self.nbirwcost,
                   self.nbitotcost, self.nbiyrcost, self.nstatecode, self.n_fhwa_reg, self.bb_pct, self.bb_brdgeid,
                   self.defhwy, self.paralstruc, self.trafficdir, self.tempstruc, self.nhs_ind, self.fedlandhwy,
                   self.yearrecon, self.dkstructyp, self.dksurftype, self.dkmembtype, self.dkprotect, self.truckpct,
                   self.trucknet, self.pierprot, self.nbislen, self.scourcrit, self.adtfuture, self.adtfutyear,
                   self.lftbrnavcl, self.reserved, self.nbi_rating, self.suff_prefx, self.suff_rate]

        return self.full_bridge_id + '\t' + ','.join(results)

def main(argv):
    # nbias_data = open('data/nbias_data_00000.txt', 'r')
    # lines = nbias_data.readlines()
    # mapper_max_raw_data_output = open('data/1_mapper_brinsp_data_metric_output.txt', 'w')
    # for line in lines:

    for line in sys.stdin:
        line = line.rstrip()
        data_raw = NBIAS_data_metric(line)
        print data_raw.toString()
        # mapper_max_raw_data_output.write(data_raw.toString() + '\n')

    # nbias_data.close()
    # mapper_max_raw_data_output.close()

if __name__ == '__main__':
    main(sys.argv)