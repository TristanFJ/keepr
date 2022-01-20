import{k,C as V,c as i,a as l,o,b as r,d as e,t as n,j as _,m as p,F as m,l as u,e as w,D as f}from"./vendor.9cbc420e.js";import{_ as x,p as h,k as P,A as t,l as b,P as B}from"./index.d2f3ff0e.js";const K={name:"Profile",setup(){const d=k();return V(async()=>{try{await h.getProfile(d.params.id),await P.getByProfile(t.profile.id),await h.getVaults(t.profile.id)}catch(c){b.error(c),B.toast(c.message,"error")}}),{profile:i(()=>t.profile),keeps:i(()=>t.keeps),vaults:i(()=>t.profileVaults),account:i(()=>t.account)}}},N={class:"profile-page p-0 container-fluid"},S={class:"row m-3"},j={class:"col d-flex"},C=["src"],M={class:"m-3"},A={class:"container-fluid"},D={class:"row"},F={class:"m-0 my-4 py-4 bg-primary"},E={key:0,class:"mdi mdi-plus text-info selectable rounded"},L={class:"masonry-with-columns"},R={class:"container-fluid"},T={class:"row"},q={class:"m-0 my-4 py-4 bg-primary"},z={key:0,class:"mdi mdi-plus text-info selectable rounded"},G={class:"masonry-with-columns"};function H(d,c,I,s,J,O){const v=l("Vault"),y=l("Keep"),g=l("KeepModal");return o(),r("div",N,[e("div",S,[e("div",j,[e("img",{class:"rounded-pill",src:s.profile.picture,alt:""},null,8,C),e("h3",M,n(s.profile.name),1)])]),e("div",A,[e("div",D,[e("h1",F,[_(" Vaults: "+n(s.vaults.length)+" ",1),s.profile.id===s.account.id?(o(),r("i",E)):p("",!0)]),e("div",L,[(o(!0),r(m,null,u(s.vaults,a=>(o(),f(v,{vault:a,key:a.id},null,8,["vault"]))),128))])])]),e("div",R,[e("div",T,[e("h1",q,[_(" Keeps: "+n(s.keeps.length)+" ",1),s.profile.id===s.account.id?(o(),r("i",z)):p("",!0)]),e("div",G,[(o(!0),r(m,null,u(s.keeps,a=>(o(),f(y,{keep:a,key:a.id},null,8,["keep"]))),128))])])]),w(g)])}var W=x(K,[["render",H]]);export{W as default};
